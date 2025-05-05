//using CSCI3110KRTERMPROJ.Models.ViewModels;
//using CSCI3110KRTERMPROJ.Models;
using CSCI3110KRTERMPROJ.Services;
//using System;

namespace CSCI3110KRTERMPROJ.Services;

public class RoleSkillRepository
{
    private readonly ApplicationDbContext _db;

    public RoleSkillRepository(ApplicationDbContext db)
    {
        _db = db;
    }
}

