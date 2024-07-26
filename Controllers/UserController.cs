using UserRegistration.DAL;
using UserRegistration.Models;
using Microsoft.AspNetCore.Mvc;

namespace UserRegistration.Controllers
{
    public class UserController : Controller
    {
        private readonly User_DAL _dal;

        public UserController(User_DAL dal)
        {
            _dal = dal;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<User> users = new List<User>();
            try
            {
                users = _dal.GetAll();
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }
            return View(users);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model data is invalid";
                    return View(model);
                }
                bool result = _dal.Insert(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to save the data";
                    return View(model);
                }
                TempData["successMessage"] = "User details saved";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            try
            {
                User user = _dal.GetById(id);
                if (user.UserId == 0)
                {
                    TempData["errorMessage"] = $"User details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Edit(User model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["errorMessage"] = "Model datais invalid";
                    return View(model);
                }
                bool result = _dal.Update(model);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to update the data";
                    return View(model);
                }
                TempData["successMessage"] = "User details updated";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                User user = _dal.GetById(id);
                if (user.UserId == 0)
                {
                    TempData["errorMessage"] = $"User details not found with Id: {id}";
                    return RedirectToAction("Index");
                }
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                bool result = _dal.Delete(id);
                if (!result)
                {
                    TempData["errorMessage"] = "Unable to delete the data";
                    return RedirectToAction("Delete", new { id });
                }
                TempData["successMessage"] = "User details deleted";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}

