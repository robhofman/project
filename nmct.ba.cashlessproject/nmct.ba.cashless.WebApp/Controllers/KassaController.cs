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
    public class KassaController : Controller
    {
        // GET: Kassa
        [HttpGet]
        public ActionResult Index(int? org)
        {
            List<Organisation> organisaties = VerenigingenDA.GetOrganisations();
            ViewBag.Organisations = organisaties;

            //kassas voor een bepaalde vereniging
            if (org.HasValue)
            {
                List<RegisterITCompany> registers = KassasDA.GetRegistersByOrganisation(org.Value);
                ViewBag.Registers = registers;
            }
            List<RegisterITCompany> availableRegisters = KassasDA.GetAvailableRegisters();
            ViewBag.AvailableRegisters = availableRegisters;
            return View();
        }
        [HttpGet]
        public ActionResult Update(int id)
        {
            RegisterITCompany register = KassasDA.GetRegisterById(id);
            int orgId = KassasDA.GetOrganisationIdByRegisterId(id);
            if (orgId != 0)
            {
                Organisation org = VerenigingenDA.GetOrganisationById(orgId);
                register.OrganisationName = org.OrganisationName;
            }
            return View(register);
        }

        [HttpPost]
        public ActionResult Update(RegisterITCompany reg)
        {

            if (reg.OrganisationName == null)
            {
                KassasDA.DeleteFromOrgRegtable(reg);
            }
            else
            {
                KassasDA.UpdateRegister(reg);
                KassasDA.UpdateOrgAndReg(reg);
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Toekennen(int id)
        {
            RegisterITCompany register = KassasDA.GetRegisterById(id);
            int orgId = KassasDA.GetOrganisationIdByRegisterId(id);

            return View(register);
        }

        [HttpPost]
        public ActionResult Toekennen(RegisterITCompany reg)
        {
            KassasDA.KassaToekennen(reg);
            return RedirectToAction("Index");
        }
    }
}