using Spedizioni.Models;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace AziendaSpedizioni.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(Utenti u)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["Speed"].ConnectionString.ToString();
            using (SqlConnection conn = new SqlConnection(connectionString))
                try
                {
                    conn.Open();
                    string query = "SELECT * FROM Utenti WHERE Username = @Username AND Password = @Password";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Username", u.Usename);
                    cmd.Parameters.AddWithValue("@Password", (u.Password));
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        Session["Username"] = u.Usename;
                        Session["Role"] = dr["Role"].ToString();
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Username o password errati";
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = ex.Message;

                    return View();
                }
        }

        public ActionResult CheckCodFiscale(string CodFisc)
        {
            string pattern = @"^[A-Z]{6}\d{2}[A-Z]\d{2}[A-Z]\d{3}[A-Z]$";
            Regex regex = new Regex(pattern);
            if (regex.IsMatch(CodFisc))
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult CheckPIVA(string PIVA)
        {
            {
                string pattern = @"^\d{11}$";
                Regex regex = new Regex(pattern);
                if (regex.IsMatch(PIVA))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }
        DateTime miaData = DateTime.Today;
        public ActionResult checkDataSpedizione(DateTime data)
        {
            DateTime oggi = DateTime.Today;
            miaData = data;
            if (data >= oggi)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult checkDataConsegna(DateTime data)
        {
            if (data > miaData)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult InserisciSpedizione()
        {
            return View();
        }
        [HttpPost]
        public ActionResult InserisciSpedizione(SpedizioniCS s)
        {
            return View();
        }
    }
}