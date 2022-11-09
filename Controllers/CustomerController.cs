using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCartMVC.Models;
using Newtonsoft.Json;
using System.Text;

namespace MyCartMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly DeliveryManagementSystemContext dd;
        private readonly ISession session;

        public CustomerController(DeliveryManagementSystemContext _dd, IHttpContextAccessor httpContextAccessor)
        {
            dd= _dd;
            session = httpContextAccessor.HttpContext.Session;

        }
        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(CustomerRegistration e)
        {
            using (var client = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(e), Encoding.UTF8, "application/json");
                using (var response = await client.PostAsync("https://localhost:44374/api/Customer/AddRegistration", content))
                {
                    string apiresponse = await response.Content.ReadAsStringAsync();
                    var empobj = JsonConvert.DeserializeObject<CustomerRegistration>(apiresponse);
                }
            }
            return RedirectToAction("Login");
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public  IActionResult Login(CustomerRegistration e)
        {
            // HttpContext.Session.SetString("Uno", dd.CustomerRegistrations);
            using (var client = new HttpClient())
            {
             using (var response = client.GetAsync("https://localhost:44374/api/Customer/AddLogin?username="+e.CustomerName+"&password="+e.Password).Result)
                {
                    string apiresponse = response.Content.ReadAsStringAsync().Result;
                  //string json = JsonConvert.SerializeObject(apiresponse, Formatting.Indented);
                    var empobj = JsonConvert.DeserializeObject<CustomerRegistration>(apiresponse);
                    if (empobj!=null)
                    {
                        ViewBag.SuccessMessage="You have logged in successfully!!!";
                        HttpContext.Session.SetInt32("cusid", empobj.CustomerId);
                        HttpContext.Session.SetString("Cname", empobj.CustomerName);
                        return RedirectToAction("Home");
                    }
                    else
                    {
                        ViewBag.FailureMessage="Invalid UserName or Password";
                        return View();

                    }
                }
               
            }
            

        }
        public IActionResult RegLogPage()
        {
            return View();
        }
        
        public IActionResult Home()
        {
            return View();
        }
    
        [HttpGet]
        public IActionResult AddBooking()
        {

            var cusname = HttpContext.Session.GetString("Cname");
            if (cusname!=null)
            {
                // return View("AddBooking");
                return View();
            }
            else
            {
                return RedirectToAction ("Login");
            }
              
            
         
        }



        [HttpPost]
        public IActionResult AddBooking(AddBooking a)
        {
            // var Myid= HttpContext.Session.GetString("cusid");
            // a.CustomerId=int.Parse(Myid);

            var Eid = HttpContext.Session.GetInt32("Empid");
            var cusid = HttpContext.Session.GetInt32("cusid");
            a.CustomerId=cusid;
            a.ExecutiveId=Eid;
             dd.AddBookings.Add(a);
              dd.SaveChanges();
             return View("success");
                
            }
          
           




            // ViewBag.vval=a.CustomerId;

            // List<int> exe = (from i in dd.ExecutiveRegistrations
            //          select i.ExecutiveId).ToList();
            //var final = dd.AddBookings.Where(val=>val.CustomerId==a.CustomerId).ToList();
            //return View("viewBooking", final);
        

             
        public IActionResult success()
        {
            var cusname = HttpContext.Session.GetString("Cname");
            if (cusname!=null)
            {
               
                return View();
            }
            else
            {

                return RedirectToAction("Login");
            }
        }
        public IActionResult ViewBooking()
        {
            var cusname = HttpContext.Session.GetString("Cname");
            if (cusname!=null)
            {
                var ans = HttpContext.Session.GetInt32("cusid");

                var result = dd.AddBookings.Where(res => res.CustomerId==ans).ToList();

                return View("ViewBooking", result);

            }
            else
            {
                return RedirectToAction("Login");
            }

            

        }




        public IActionResult DeleteBooking()

        {
            var cusname = HttpContext.Session.GetString("Cname");
            if (cusname!=null)
            {

                var del = HttpContext.Session.GetInt32("cusid");
                var e = dd.AddBookings.ToList();
                var res = dd.AddBookings.Where(var => var.CustomerId==del);
                return View(res);
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        
        
        public IActionResult DeleteConfirmed(int id)
        {
           // var del = HttpContext.Session.GetInt32("cusid");
            AddBooking e = dd.AddBookings.Find(id);
            dd.AddBookings.Remove(e);
            dd.SaveChanges();
            return View("DeleteSuccess");
            ////return RedirectToAction("ViewBooking");

        }
        public IActionResult DeleteSuccess()
        {
            var cusname = HttpContext.Session.GetString("Cname");
            if (cusname!=null)
            {

                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        //public IActionResult DeleteBooking(int OrderId)

        //{

        //    var result = dd.AddBookings.Where(res => res.CustomerId==ans);
        //    AddBooking e = dd.AddBookings.Find(OrderId);
        //     return View("DeleteBooking", e);

        //}
        //[HttpPost]
        //[ActionName("DeleteBooking")]

        //public IActionResult DeleteConfirmed(int OrderId)
        //{
        //  var sss=  HttpContext.Session.GetInt32("Orderid");
        //    var result = dd.AddBookings.Where(res => res.OrderId==sss);
        //    return View("DeleteBooking", result);



        //}
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }


    }
}
