using AminDashboard.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AminDashboard.Controllers
{
    public class RoleController : Controller
    {
        
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            
            _roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles =await _roleManager.Roles.ToListAsync();

            return View(roles);
        }

        [HttpPost]
        public async Task<IActionResult> Create(RoleFormViewModel model)
        {
            if(ModelState.IsValid)
            {
                var roleExist = await _roleManager.RoleExistsAsync(model.Name);

                if(!roleExist) 
                {
                    await _roleManager.CreateAsync(new IdentityRole { Name = model.Name.Trim()});
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Name", "Role Is Exist");
                    return View(nameof(Index), await _roleManager.Roles.ToListAsync());
                }
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(string id)
        {
            var role =await _roleManager.FindByIdAsync(id);

            await _roleManager.DeleteAsync(role);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var mappedRole = new RoleViewModel()
            { 
                Name = role.Name,
            };
            return View(mappedRole);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromRoute]string id,RoleViewModel model)
        {
            if (id != model.Id)
                return BadRequest();
            if(ModelState.IsValid)
            {
                var roleExist=await _roleManager.RoleExistsAsync(model.Name);
                if(!roleExist)
                {
                    var role = await _roleManager.FindByIdAsync(model.Id);
                    role.Name = model.Name.ToUpper();
                    role.Name = model.Name;
                    await _roleManager.UpdateAsync(role);
                    return RedirectToAction(nameof(Index));
                }
                else 
                ModelState.AddModelError("Name", "Role Is Exist");
                return View(nameof(Index), await _roleManager.Roles.ToListAsync());
            }
            return View(model);
        }
    }
}
