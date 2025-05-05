using CSCI3110KRTERMPROJ.Models;
using CSCI3110KRTERMPROJ.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CSCI3110KRTERMPROJ.Controllers;

[Authorize]
public class PersonRoleController : Controller
{
    private readonly IPersonRoleRepository _personroleRepo;

    public PersonRoleController(IPersonRoleRepository personroleRepo)
    {
        _personroleRepo = personroleRepo;
    }

    //PersonRole Delete
    public async Task<IActionResult> Delete(int id)
    {
        var personrole = await _personroleRepo.GetByIdAsync(id);
        if (personrole == null)
        {
            return RedirectToAction("Index");
        }

        return View(personrole); // Return a confirmation view
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _personroleRepo.DeleteAsync(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    //Role links for a specific Person
    public async Task<IActionResult> GetByPersonId(int personId)
    {
        var personrole = await _personroleRepo.GetByPersonIdAsync(personId);
        if (personrole == null)
        {
            return RedirectToAction("Index");
        }

        return View(personrole);
    }

    [HttpGet]
    //Person links for a specific Role
    public async Task<IActionResult> GetByRoleId(int id)
    {
        var personrole = await _personroleRepo.GetByRoleIdAsync(id);
        if (personrole == null)
        {
            return RedirectToAction("Index");
        }

        return View(personrole);
    }


}
