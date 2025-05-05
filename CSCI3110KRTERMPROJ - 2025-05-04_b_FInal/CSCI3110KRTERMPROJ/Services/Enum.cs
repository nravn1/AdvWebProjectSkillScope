//using CSCI3110KRTERMPROJ.Models;
using Microsoft.EntityFrameworkCore;

namespace CSCI3110KRTERMPROJ.Services
{
    public enum AssignmentStatus
    {
        Success,
        NoPersonExists,
        NoPrivilegeExists,
        NoSkillExists,
        NoRoleExists,
        LinkAlreadyExists,
        LinkDoesNotExist,
        MissingId
    }
}

