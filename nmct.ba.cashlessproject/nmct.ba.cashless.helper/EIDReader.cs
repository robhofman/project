using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Web;
using System.Drawing;
using System.Windows.Media.Imaging;
using be.belgium.eid;

namespace nmct.ba.cashlessproject.helper
{
    public class EIDReader
    {
        #region Scan EID
        public EIDReader()
        {
            ScanEID();
        }

        public void ScanEID()
        {
            try
            {
                // initialising and declaring BEID card
                BEID_ReaderSet.initSDK();
                BEID_ReaderSet readerSet = BEID_ReaderSet.instance();
                BEID_ReaderContext reader = readerSet.getReader(); ;

                // check different card types
                if (reader.isCardPresent())
                {
                    if (CheckDifferentPassports(reader))
                        Load_eid(reader);
                    else if (reader.getCardType() == BEID_CardType.BEID_CARDTYPE_SIS)
                        System.Diagnostics.Debug.WriteLine("Load_sis(Reader)");
                    else
                        System.Diagnostics.Debug.WriteLine("UNKNOWN CARD");
                }
            }
            catch (BEID_Exception ex)
            {
                BEID_ReaderSet.releaseSDK();
            }
            catch (Exception ex)
            {
                BEID_ReaderSet.releaseSDK();
            }
            finally
            {
                BEID_ReaderSet.releaseSDK();
            }
        }

        private static bool CheckDifferentPassports(BEID_ReaderContext reader)
        {
            return reader.getCardType() == BEID_CardType.BEID_CARDTYPE_EID ||
                reader.getCardType() == BEID_CardType.BEID_CARDTYPE_FOREIGNER ||
                reader.getCardType() == BEID_CardType.BEID_CARDTYPE_KIDS;
        }
        #endregion

        #region LoadEID
        private void Load_eid(BEID_ReaderContext reader)
        {
            // get EID card
            BEID_EIDCard card = reader.getEIDCard();
            if (card.isTestCard())
                card.setAllowTestCard(true);

            BEID_EId data = card.getID();
            BEID_Picture picture = card.getPicture();
            byte[] bytearray = picture.getData().GetBytes(); // picture is a array of bytes

            // set different text fields with variable "doc"
            
            string naam = data.getSurname() + " " + data.getFirstName();
            string street = String.Format("{0} {1},", data.getStreet(), data.getAddressVersion());
            string address = String.Format("{0} {1}", data.getZipCode(), data.getMunicipality());
            string rijks = data.getNationalNumber();
            
            
            Name = naam;
            Street = street;
            Address = address;
            Picture = bytearray;
            Rijks = rijks;
        }

       
        #endregion
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _street;

        public string Street
        {
            get { return _street; }
            set { _street = value; }
        }
        private string _address;

        public string Address
        {
            get { return _address; }
            set { _address = value; }
        }
        private byte[] _picture;

        public byte[] Picture
        {
            get { return _picture; }
            set { _picture = value; }
        }
        private string _rijks;

        public string Rijks
        {
            get { return _rijks; }
            set { _rijks = value; }
        }
    }
}