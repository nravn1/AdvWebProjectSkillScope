using CSCI3110KRTERMPROJ.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using static Azure.Core.HttpHeader;
using System.Runtime.Intrinsics.X86;
using System;
using Azure;
using System.Xml.Linq;

namespace CSCI3110KRTERMPROJ.Services
{
    public class Initializer
    {
        private readonly ApplicationDbContext _db;
        public Initializer(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task SeedDatabaseAsync()
        {
            _db.Database.EnsureCreated();

            // If there are any persons then assume the database is already
            // seeded.
            if (_db.Persons.Any()) return;

            var persons = new List<Person>
            {
                new() { FName = "James", MName = "Lawrence", LName = "Smith", BirthDate = DateTime.Parse("04/12/1978") , Email = "jsmith@gmail.com", TypeCode = "Full Time", UserFlag = 'N'},
                new() { FName = "Maria", MName = "M", LName = "Garcia", BirthDate = DateTime.Parse("07/21/1982") , Email = "mmg@gmail.com", TypeCode = "Full Time", UserFlag = 'N' },
                new() { FName = "Chen", MName = "Hu", LName = "Li", BirthDate = DateTime.Parse("11/02/2002") , Email = "CL@gmail.com", TypeCode = "Intern", UserFlag = 'N' },
                new() { FName = "Aban", MName = "S", LName = "Hakim", BirthDate = DateTime.Parse("06/22/1989") , Email = "ah@gmail.com", TypeCode = "Contractor", UserFlag = 'N' },
                new() { FName = "Julian", MName = "Alexander", LName = "Brown", BirthDate = DateTime.Parse("06/19/1998") , Email = "julianpatrickharris@gmail.com", TypeCode = "Full Time", UserFlag = 'N' },
                new() { FName = "Ethan", MName = "Ryan", LName = "Sanchez", BirthDate = DateTime.Parse("09/15/2003") , Email = "ethanryansanchez@gmail.com", TypeCode = "Part Time", UserFlag = 'N' },
                new() { FName = "Olivia", MName = "Nicole", LName = "Davis", BirthDate = DateTime.Parse("02/25/2004") , Email = "olivianicodavis@hotmail.com", TypeCode = "Intern", UserFlag = 'N' },
                new() { FName = "Jackson", MName = "Cole", LName = "Marti", BirthDate = DateTime.Parse("05/22/1992") , Email = "jmartin@aol.com", TypeCode = "Contractor", UserFlag = 'N' }

            };

            await _db.Persons.AddRangeAsync(persons);
            await _db.SaveChangesAsync();

            var privileges = new List<Privilege>
            {
                new() { PrivilegeTitle = "View Persons", Description = "View all persons" },
                new() { PrivilegeTitle = "Add Person", Description = "Add a new person" },
                new() { PrivilegeTitle = "Edit Person", Description = "Edit a person" },
                new() { PrivilegeTitle = "View Privileges", Description = "View all roles" },
                new() { PrivilegeTitle = "Add Privilege", Description = "Add a new role" },
                new() { PrivilegeTitle = "Edit Privilege", Description = "Edit a role" },
                new() { PrivilegeTitle = "View Roles", Description = "View all roles" },
                new() { PrivilegeTitle = "Add Role", Description = "Add a new role" },
                new() { PrivilegeTitle = "Edit Role", Description = "Edit a role" },
                new() { PrivilegeTitle = "View Skills", Description = "View all skills" },
                new() { PrivilegeTitle = "Add Skill", Description = "Add a new skill" },
                new() { PrivilegeTitle = "Edit Skill", Description = "Edit a skill" }
            };

            await _db.Privileges.AddRangeAsync(privileges);
            await _db.SaveChangesAsync();

            var roles = new List<Role>
            {
                new() { RoleTitle = "Teller", Description = "Handles customer transactions ove the counter" },
                new() { RoleTitle = "Loan Officer", Description = "Manages loan applications and loan disbursments" },
                new() { RoleTitle = "Branch Manager", Description = "Oversees branch operations and staff performance" },
                new() { RoleTitle = "Financial Analyst", Description = "Analyzes financial data to assist in decision making" },
                new() { RoleTitle = "Treasurer", Description = "Responsible for financial management and reporting" },
                new() { RoleTitle = "Human Resources", Description = "Responsible for human resource management" },
                new() { RoleTitle = "Legal Council", Description = "Responsible for financial management and reporting" },
                new() { RoleTitle = "Manager", Description = "Oversees all operations and staff performance" },
                new() { RoleTitle = "Marketing Director", Description = "Responsible for marketing strategies and promotion" },
                new() { RoleTitle = "Software Engineer", Description = "Develops and installs softyware applications" }
            };

            await _db.Roles.AddRangeAsync(roles);
            await _db.SaveChangesAsync();

            var skills = new List<Skill>
            {
                new() { SkillTitle = "Communication Skills", Description = "Effective verbal and written communication to interact with members, staff, and external partners." },
                new() { SkillTitle = "Customer Service", Description = "Providing exceptional service to members, responding to their needs, and resolving issues in a professional manner." },
                new() { SkillTitle = "Problem-Solving", Description = "Analyzing problems, identifying solutions, and implementing effective countermeasures." },
                new() { SkillTitle = "Data Analysis", Description = "Collecting, analyzing, and interpreting data to inform business decisions." },
                new() { SkillTitle = "Financial Analysis", Description = "Ability to analyze financial data, identify trends, and make informed decisions about the credit union's operations and investments." },
                new() { SkillTitle = "Strategic Planning", Description = "Developing and implementing long-term plans to drive business growth and success." },
                new() { SkillTitle = "Leadership", Description = "Strong leadership skills to motivate and guide team members in achieving organizational goals." },
                new() { SkillTitle = "Time Management", Description = "Prioritizing tasks, managing multiple projects simultaneously, and meeting deadlines in a fast-paced environment." },
                new() { SkillTitle = "Operational Efficiency", Description = "Identifying opportunities to improve operational efficiency, reduce costs, and enhance member experience." },
                new() { SkillTitle = "Budgeting", Description = "Preparing, managing, and monitoring budgets to ensure financial stability and growth." },
                new() { SkillTitle = "Compliance", Description = "Familiarity with regulatory requirements, laws, and industry standards, and ensuring adherence to these standards." },
                new() { SkillTitle = "Risk Assessment", Description = "Identifying potential risks, assessing their likelihood and impact, and implementing strategies to mitigate them." },
                new() { SkillTitle = "Human Resources Management", Description = "Managing employee relations, recruitment, training, and development to build a high-performing team." },
                new() { SkillTitle = "Community Involvement", Description = "Engaging with local communities, participating in outreach programs, and building relationships with stakeholders." },
                new() { SkillTitle = "Cybersecurity", Description = "Protecting the credit union's systems, data, and members' information from cyber threats." },
                new() { SkillTitle = "Financial Literacy", Description = "Educating members about personal finance, financial planning, and money management." },
                new() { SkillTitle = "Investment Knowledge", Description = "Understanding investment options, risk management, and return on investment principles." },
                new() { SkillTitle = "Digital Literacy", Description = "Understanding the digital landscape, including online platforms, social media, and mobile devices." },
                new() { SkillTitle = "Change Management", Description = "Managing organizational change, communicating effectively, and supporting staff through transitions." },
                new() { SkillTitle = "Member Engagement", Description = "Understanding member needs, preferences, and behaviors to provide personalized service and build loyalty." },
                new() { SkillTitle = "Collaboration", Description = "Working effectively with external partners, vendors, and community organizations." },
                new() { SkillTitle = "Accountability", Description = "Taking ownership of actions, decisions, and outcomes, and being answerable for results." },
                new() { SkillTitle = "Innovation", Description = "Encouraging innovation, creativity, and continuous improvement within the organization." },
                new() { SkillTitle = "Teamwork", Description = "Collaborating with colleagues to achieve shared goals and objectives." },
                new() { SkillTitle = "Risk Management", Description = "Identifying potential risks, assessing their impact, and implementing strategies to mitigate them." },
                new() { SkillTitle = "Marketing Skills", Description = "Developing effective marketing strategies to attract new members, retain existing ones, and promote the credit union's products and services." }
            };

            await _db.Skills.AddRangeAsync(skills);
            await _db.SaveChangesAsync();

            var personroles = new List<PersonRole>
            {
                new() { PersonId = 7, RoleId = 10 },
                new() { PersonId = 6, RoleId = 1 },
                new() { PersonId = 3, RoleId = 1 },
                new() { PersonId = 2, RoleId = 5 },
                new() { PersonId = 1, RoleId = 8 },
                new() { PersonId = 8, RoleId = 7 },
                new() { PersonId = 4, RoleId = 10 },
                new() { PersonId = 5, RoleId = 4 }
            };

            await _db.PersonRoles.AddRangeAsync(personroles);
            await _db.SaveChangesAsync();


            var personskills = new List<PersonSkill>
            {
                new() { PersonId = 1, SkillId = 1 },
                new() { PersonId = 1, SkillId = 3 },
                new() { PersonId = 1, SkillId = 6 },
                new() { PersonId = 1, SkillId = 7 },
                new() { PersonId = 1, SkillId = 8 },
                new() { PersonId = 1, SkillId = 9 },
                new() { PersonId = 1, SkillId = 11 },
                new() { PersonId = 1, SkillId = 19 },
                new() { PersonId = 1, SkillId = 14 },
                new() { PersonId = 2, SkillId = 3 },
                new() { PersonId = 2, SkillId = 4 },
                new() { PersonId = 2, SkillId = 5 },
                new() { PersonId = 2, SkillId = 6 },
                new() { PersonId = 2, SkillId = 7 },
                new() { PersonId = 2, SkillId = 8 },
                new() { PersonId = 2, SkillId = 10 },
                new() { PersonId = 2, SkillId = 17 },
                new() { PersonId = 2, SkillId = 24 },
                new() { PersonId = 3, SkillId = 1 },
                new() { PersonId = 3, SkillId = 2 },
                new() { PersonId = 3, SkillId = 16 },
                new() { PersonId = 3, SkillId = 20 },
                new() { PersonId = 3, SkillId = 26 },
                new() { PersonId = 4, SkillId = 12 },
                new() { PersonId = 4, SkillId = 15 },
                new() { PersonId = 5, SkillId = 3 },
                new() { PersonId = 5, SkillId = 4 },
                new() { PersonId = 5, SkillId = 5 },
                new() { PersonId = 5, SkillId = 6 },
                new() { PersonId = 5, SkillId = 9 },
                new() { PersonId = 5, SkillId = 25 },
                new() { PersonId = 6, SkillId = 1 },
                new() { PersonId = 6, SkillId = 2 },
                new() { PersonId = 6, SkillId = 14 },
                new() { PersonId = 6, SkillId = 16 },
                new() { PersonId = 6, SkillId = 20 },
                new() { PersonId = 6, SkillId = 23 },
                new() { PersonId = 6, SkillId = 26 },
                new() { PersonId = 7, SkillId = 3 },
                new() { PersonId = 7, SkillId = 4 },
                new() { PersonId = 7, SkillId = 6 },
                new() { PersonId = 7, SkillId = 9 },
                new() { PersonId = 7, SkillId = 12 },
                new() { PersonId = 7, SkillId = 15 },
                new() { PersonId = 7, SkillId = 21 },
                new() { PersonId = 7, SkillId = 24 },
                new() { PersonId = 7, SkillId = 23 },
                new() { PersonId = 8, SkillId = 1 },
                new() { PersonId = 8, SkillId = 6 },
                new() { PersonId = 8, SkillId = 11 },
                new() { PersonId = 8, SkillId = 12 },
                new() { PersonId = 8, SkillId = 13 },
                new() { PersonId = 8, SkillId = 19 },
                new() { PersonId = 8, SkillId = 21 },
                new() { PersonId = 8, SkillId = 25 }
            };

            await _db.PersonSkills.AddRangeAsync(personskills);
            await _db.SaveChangesAsync();


            var personprivileges = new List<PersonPrivilege>
            {
                new() { PersonId = 8, PrivilegeId = 10 },
                new() { PersonId = 8, PrivilegeId = 11 },
                new() { PersonId = 8, PrivilegeId = 12 },
                new() { PersonId = 8, PrivilegeId = 1 },
                new() { PersonId = 8, PrivilegeId = 2 },
                new() { PersonId = 8, PrivilegeId = 3 },
                new() { PersonId = 7, PrivilegeId = 1 },
                new() { PersonId = 7, PrivilegeId = 7 },
                new() { PersonId = 6, PrivilegeId = 1 },
                new() { PersonId = 6, PrivilegeId = 10 },
                new() { PersonId = 5, PrivilegeId = 1 },
                new() { PersonId = 5, PrivilegeId = 7 },
                new() { PersonId = 5, PrivilegeId = 10 },
                new() { PersonId = 4, PrivilegeId = 1 },
                new() { PersonId = 3, PrivilegeId = 1 },
                new() { PersonId = 3, PrivilegeId = 10 },
                new() { PersonId = 1, PrivilegeId = 1 },
                new() { PersonId = 1, PrivilegeId = 4 },
                new() { PersonId = 1, PrivilegeId = 7 },
                new() { PersonId = 1, PrivilegeId = 10 },
                new() { PersonId = 1, PrivilegeId = 11 },
                new() { PersonId = 1, PrivilegeId = 12 },
                new() { PersonId = 2, PrivilegeId = 1 },
                new() { PersonId = 2, PrivilegeId = 7 },
                new() { PersonId = 2, PrivilegeId = 10 }
            };

            await _db.PersonPrivileges.AddRangeAsync(personprivileges);
            await _db.SaveChangesAsync();


            var roleskills = new List<RoleSkill>
            {
                new() { RoleId = 10, SkillId = 22 },
                new() { RoleId = 10, SkillId = 18 },
                new() { RoleId = 10, SkillId = 15 },
                new() { RoleId = 10, SkillId = 8 },
                new() { RoleId = 10, SkillId = 4 },
                new() { RoleId = 9, SkillId = 1 },
                new() { RoleId = 9, SkillId = 6 },
                new() { RoleId = 9, SkillId = 7 },
                new() { RoleId = 9, SkillId = 14 },
                new() { RoleId = 9, SkillId = 20 },
                new() { RoleId = 9, SkillId = 24 },
                new() { RoleId = 1, SkillId = 1 },
                new() { RoleId = 1, SkillId = 2 },
                new() { RoleId = 1, SkillId = 20 },
                new() { RoleId = 1, SkillId = 24 },
                new() { RoleId = 2, SkillId = 1 },
                new() { RoleId = 2, SkillId = 2 },
                new() { RoleId = 2, SkillId = 5 },
                new() { RoleId = 3, SkillId = 1 },
                new() { RoleId = 3, SkillId = 2 },
                new() { RoleId = 3, SkillId = 3 },
                new() { RoleId = 3, SkillId = 7 },
                new() { RoleId = 3, SkillId = 8 },
                new() { RoleId = 3, SkillId = 21 },
                new() { RoleId = 3, SkillId = 24 },
                new() { RoleId = 8, SkillId = 1 },
                new() { RoleId = 8, SkillId = 3 },
                new() { RoleId = 8, SkillId = 6 },
                new() { RoleId = 8, SkillId = 7 },
                new() { RoleId = 8, SkillId = 8 },
                new() { RoleId = 8, SkillId = 19 },
                new() { RoleId = 8, SkillId = 24 },
                new() { RoleId = 4, SkillId = 5 },
                new() { RoleId = 4, SkillId = 4 },
                new() { RoleId = 4, SkillId = 17 },
                new() { RoleId = 4, SkillId = 25 },
                new() { RoleId = 5, SkillId = 3 },
                new() { RoleId = 5, SkillId = 4 },
                new() { RoleId = 5, SkillId = 5 },
                new() { RoleId = 5, SkillId = 6 },
                new() { RoleId = 5, SkillId = 7 },
                new() { RoleId = 5, SkillId = 10 },
                new() { RoleId = 5, SkillId = 25 },
                new() { RoleId = 6, SkillId = 13 },
                new() { RoleId = 6, SkillId = 21 },
                new() { RoleId = 6, SkillId = 11 },
                new() { RoleId = 6, SkillId = 8 },
                new() { RoleId = 6, SkillId = 6 },
                new() { RoleId = 7, SkillId = 11 },
                new() { RoleId = 7, SkillId = 25 },
                new() { RoleId = 7, SkillId = 1 }
            };

            await _db.RoleSkills.AddRangeAsync(roleskills);
            await _db.SaveChangesAsync();


        }

    }
}
