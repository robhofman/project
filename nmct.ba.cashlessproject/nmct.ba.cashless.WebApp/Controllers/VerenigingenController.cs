using nmct.ba.cashless.helper;
using nmct.ba.cashless.WebApp.DataAccess;
using nmct.ba.cashlessproject.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashless.WebApp.Controllers
{
    [Authorize]
    public class VerenigingenController : Controller
    {
        // GET: Verenigingen
        [HttpGet]
        public ActionResult Index()
        {
            List<Organisation> organisations = new List<Organisation>();
            organisations = VerenigingenDA.GetOrganisations();
            ViewBag.Organisations = organisations;
            return View();
        }

        [HttpGet]
        public ActionResult Toevoegen()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Toevoegen(Organisation o)
        {
            if (ModelState.IsValid)
            {
                var org = new Organisation
                {
                    Login = Cryptography.Encrypt(o.Login),
                    Password = Cryptography.Encrypt(o.Password),
                    DbName = Cryptography.Encrypt(o.DbName),
                    Dblogin = Cryptography.Encrypt(o.Dblogin),
                    Dbpassword = Cryptography.Encrypt(o.Dbpassword),
                    OrganisationName = o.OrganisationName,
                    Address = o.Address,
                    Email = o.Email,
                    Phone = o.Phone
                };

                VerenigingenDA.InsertOrganisation(org);

            }

            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            Organisation o = VerenigingenDA.GetOrganisationById(id);

            o.Login = Cryptography.Decrypt(o.Login);
            o.Password = Cryptography.Decrypt(o.Password);
            o.Dblogin = Cryptography.Decrypt(o.Dblogin);
            o.DbName = Cryptography.Decrypt(o.DbName);
            o.Dbpassword = Cryptography.Decrypt(o.Dbpassword);


            return View(o);
        }

        [HttpPost]
        public ActionResult Update(Organisation o)
        {
            o.Login = Cryptography.Encrypt(o.Login);
            o.Password = Cryptography.Encrypt(o.Password);
            o.Dblogin = Cryptography.Encrypt(o.Dblogin);
            o.DbName = Cryptography.Encrypt(o.DbName);
            o.Dbpassword = Cryptography.Encrypt(o.Dbpassword);

            VerenigingenDA.UpdateOrganisation(o);
            return RedirectToAction("Index");
        }

    }
}