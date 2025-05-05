'use strict';

const assignPrivilegeModalDOM = document.getElementById('assignPrivilegeModal');
const assignPrivilegeModal = new bootstrap.Modal(assignPrivilegeModalDOM);
const assignSkillModalDOM = document.getElementById('assignSkillModal');
const assignSkillModal = new bootstrap.Modal(assignSkillModalDOM);
const assignRoleModalDOM = document.getElementById('assignRoleModal');
const assignRoleModal = new bootstrap.Modal(assignRoleModalDOM);

document.querySelectorAll('a.btn-link').forEach(link => {
    link.addEventListener('click', async (e) => {
        e.preventDefault();

        const section = link.getAttribute('data-section');
        const parentTitle = link.getAttribute('data-persontitle');
        //console.log(section);

        // Get personId or roleId from the href, e.g. /person/assignprivilege/3 where the 3 is the Id
        const urlParts = link.href.split('/');
        const parentId = urlParts[urlParts.length - 1];

        switch (section) {
            case 'personprivilege':
                assignPrivilegeModal.show();

                try {
                    const privileges = await getAllPersonPrivileges();
                    //console.log('Privileges:', privileges);
                    populatePrivileges(privileges, parentId, parentTitle);
                } catch (error) {
                    console.error('Error loading privileges for a person:', error);
                }
                break;
            case 'personskill':
                assignSkillModal.show();

                try {
                    const skills = await getAllPersonSkills();
                    //console.log('Skills:', skills);
                    populateSkills(skills, parentId, parentTitle);
                } catch (error) {
                    console.error('Error loading skills for a person:', error);
                }
                break;
            case 'personrole':
                assignRoleModal.show();

                try {
                    const roles = await getAllPersonRoles();
                    //console.log('Roles:', roles);
                    populateRoles(roles, parentId, parentTitle);
                } catch (error) {
                    console.error('Error loading roles for a person:', error);
                }
                break;
            default:
                console.error('Error - Unknown data-section from Person or Role Index button link:', error);
        }
    });
});

//Listeners for the CLOSE button on the popup peron windows - closes modal popup and reload page
document.getElementById('btnAssignPrivilegeClose').addEventListener('click', () => {
    assignPrivilegeModal.hide();
    location.reload();    //reload the current webpage so update counts get displayed
});
document.getElementById('btnAssignSkillClose').addEventListener('click', () => {
    assignSkillModal.hide();
    location.reload();    //reload the current webpage so update counts get displayed
});
document.getElementById('btnAssignRoleClose').addEventListener('click', () => {
    assignRoleModal.hide();
    location.reload();    //reload the current webpage so update counts get displayed
});

//Get all items needed in the modal popup from the Controller
async function getAllPersonPrivileges() {
    try {
        const res = await fetch('/privilege/getall');
        const privileges = await res.json();
        //console.log(privileges);
        return privileges;
    } catch (err) {
        console.error(err);
    }
}

async function getAllPersonSkills() {
    try {
        const res = await fetch('/skill/getallforperson');
        const skills = await res.json();
        //console.log(skills);
        return skills;
    } catch (err) {
        console.error(err);
    }
}

async function getAllPersonRoles() {
    try {
        const res = await fetch('/role/getallforperson');
        const roles = await res.json();
        console.log(roles);
        return roles;
    } catch (err) {
        console.error(err);
    }
}

//Find element id 'privilegeTableBody' in the modal partial View class, then populate with the data and event controls
function populatePrivileges(privileges, personId, personTitle) {

    //Find modal title element and insert the person title
    const ttitle = document.getElementById('assignPrivilegeModalTitle');
    ttitle.innerHTML = '"' + personTitle + '"' + ' Privileges:';

    //Find the table body element and insert privileges
    const tbody = document.getElementById('privilegeTableBody');
    tbody.innerHTML = '';

    //build array of privileges already assigned to person
    const myPrivileges = [];
    privileges.forEach(privilege => {
        privilege.personsWithThisPrivilege.forEach(personWithThisPrivilege => {
            const linkPersonId = personWithThisPrivilege.personId;
            if (personId == linkPersonId) {
                console.log('Privilege Id For Person:', privilege.privilegeId);
                myPrivileges.push(privilege.privilegeId);
            }
        });
    });
    console.log('myPrivilege Array: ', myPrivileges);

    //build list of privileges to Assign but create an Unassign for those already assigned (privileges already in the myPrivileges array)
    privileges.forEach(privilege => {
        const row = document.createElement('tr');

        if (myPrivileges.includes(privilege.privilegeId)) {
            row.innerHTML = `
                <td>${personId}</td>
                <td>${privilege.privilegeId}</td>
                <td><b>${privilege.privilegeTitle}</b></td>
                <td><button class="btn btn-warning unassign-btn" data-link-id="${privilege.id}" data-person-id="${personId}" data-privilege-id="${privilege.privilegeId}">Unassign</button></td>
        `   ;
        } else {
            row.innerHTML = `
                <td>${personId}</td>
                <td>${privilege.privilegeId}</td>
                <td>${privilege.privilegeTitle}</td>
                <td><button class="btn btn-warning assign-btn" data-person-id="${personId}" data-privilege-id="${privilege.privilegeId}">Assign</button></td>
    `       ;
        };

        tbody.appendChild(row);
    });


    // Hook up the assign buttons
    document.querySelectorAll('.assign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const personId = btn.dataset.personId;
            const privilegeId = btn.dataset.privilegeId;

            const result = await assignPrivilegeToPerson(personId, privilegeId);

            if (result === "Ok") {
                alert("Privilege successfully assigned to person.");
                //assignPrivilegeModal.hide();
                //location.reload();
            } else {
                alert(result.message || "Failed to assign privilege to a person.");
            }
        });
    });

    // Hook up the unassign buttons listeners
    document.querySelectorAll('.unassign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const personId = btn.dataset.personId;
            const privilegeId = btn.dataset.privilegeId;

            const result = await unAssignPrivilegeFromPerson(personId, privilegeId);

            if (result === "Ok") {
                alert("Privilege successfully unassigned from person.");
                //assignSkillModal.hide();
                //location.reload();
            } else {
                alert(result.message || "Failed to unassign privilege from person.");
            }
        });
    });
}

//Find element id 'skillTableBody' in the modal partial View class, then populate with the data and event controls
function populateSkills(skills, personId, personTitle) {

    //Find modal title element and insert the person title
    const ttitle = document.getElementById('assignSkillModalTitle');
    ttitle.innerHTML = '"' + personTitle + '"' + ' Skills:';

    //Find the table body element and insert skills
    const tbody = document.getElementById('skillTableBody');
    tbody.innerHTML = '';

    //build array of skills assigned to person
    const mySkills = [];
    skills.forEach(skill => {
        skill.personsWithThisSkill.forEach(personWithThisSkill => {
            const linkPersonId = personWithThisSkill.personId;  // Access personId
            if (personId == linkPersonId) {
                console.log('Skill Id For Person:', skill.skillId);
                mySkills.push(skill.skillId);
            }
        });
    });
    console.log('mySkill Array: ', mySkills);

    //build list of skills to Assign but create an Unassign for those already assigned (skills in the mySkills array) 
    skills.forEach(skill => {
        const row = document.createElement('tr');

        if (mySkills.includes(skill.skillId)) {
            row.innerHTML = `
                <td>${personId}</td>
                <td>${skill.skillId}</td>
                <td><b>${skill.skillTitle}</b></td>
                <td><button class="btn btn-warning unassign-btn" data-link-id="${skill.id}" data-person-id="${personId}" data-skill-id="${skill.skillId}">Unassign</button></td>
        `   ;
        } else {
            row.innerHTML = `
                <td>${personId}</td>
                <td>${skill.skillId}</td>
                <td>${skill.skillTitle}</td>
                <td><button class="btn btn-warning assign-btn" data-person-id="${personId}" data-skill-id="${skill.skillId}">Assign</button></td>
    `       ;
        };

        tbody.appendChild(row);
    });

    // Hook up the assign buttons
    document.querySelectorAll('.assign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const personId = btn.dataset.personId;
            const skillId = btn.dataset.skillId;

            const result = await assignSkillToPerson(personId, skillId);

            if (result === "Ok") {
                alert("Skill successfully assigned to person.");
            } else {
                alert(result.message || "Failed to assign skill to a person.");
            }
        });
    });

    // Hook up the unassign buttons listeners
    document.querySelectorAll('.unassign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const personId = btn.dataset.personId;
            const skillId = btn.dataset.skillId;

            const result = await unAssignSkillFromPerson(personId, skillId);

            if (result === "Ok") {
                alert("Skill successfully unassigned from person.");
            } else {
                alert(result.message || "Failed to unassign skill from person.");
            }
        });
    });
}

//Find element id in the modal partial View class, then populate with the data and event controls
function populateRoles(roles, personId, personTitle) {

    //Find modal title element and insert the person title
    const ttitle = document.getElementById('assignRoleModalTitle');
    ttitle.innerHTML = '"' + personTitle + '"' + ' Roles:';

    //Find the table body element and insert roles
    const tbody = document.getElementById('roleTableBody');
    tbody.innerHTML = '';

    //build array of roles already assigned to person
    const myRoles = [];
    roles.forEach(role => {
        role.personsWithRole.forEach(personWithRole => {
            const linkPersonId = personWithRole.personId;  // Access personId
            if (personId == linkPersonId) {
                console.log('Role Id For Person:', role.roleId);
                myRoles.push(role.roleId);
            }
        });
    });
    console.log('myRole Array: ', myRoles);

    //build list of roles to Assign but create an Unassign for those already assigned (roles in the myRoles array) 
    roles.forEach(role => {
        const row = document.createElement('tr');

        //If this role already exists in the persons myRoles array, set it up so it can be UnAssigned, otherwise let is be assigned 
        if (myRoles.includes(role.roleId)) {
            row.innerHTML = `
                <td>${personId}</td>
                <td>${role.roleId}</td>
                <td><b>${role.roleTitle}</b></td>
                <td><button class="btn btn-warning unassign-btn"  data-link-id="${role.id}" data-person-id="${personId}" data-role-id="${role.roleId}">Unassign</button></td>
        `   ;
        } else {
            row.innerHTML = `
                <td>${personId}</td>
                <td>${role.roleId}</td>
                <td>${role.roleTitle}</td>
                <td><button class="btn btn-warning assign-btn" data-person-id="${personId}" data-role-id="${role.roleId}">Assign</button></td>
    `       ;
        };

        tbody.appendChild(row);
    });

    // Hook up the assign person/role buttons
    document.querySelectorAll('.assign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const personId = btn.dataset.personId;
            const roleId = btn.dataset.roleId;

            const result = await assignRoleToPerson(personId, roleId);

            if (result === "Ok") {
                alert("Role successfully assigned to person.");
             } else {
                alert(result.message || "Failed to assign role to a person.");
            }
        });
    });

    // Hook up the unassign buttons listeners
    document.querySelectorAll('.unassign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const personId = btn.dataset.personId;
            const roleId = btn.dataset.roleId;

            const result = await unAssignRoleFromPerson(personId, roleId);

            if (result === "Ok") {
                alert("Role successfully unassigned from person.");
             } else {
                alert(result.message || "Failed to unassign role from person.");
            }
        });
    });

}


//Insert the assignment link into database via the Contoller
async function assignPrivilegeToPerson(personId, privilegeId) {
    try {
        /*handled in PersonController*/
        const res = await fetch(`/person/assignprivilege/${personId}?privilegeId=${privilegeId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Person/Privilege assignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}

async function assignSkillToPerson(personId, skillId) {
    try {
        /*handled in PersonController*/
        const res = await fetch(`/person/assignskill/${personId}?skillId=${skillId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Person/Skill assignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}

async function assignRoleToPerson(personId, roleId) {
    try {
        /*handled in PersonController*/
        const res = await fetch(`/person/assignrole/${personId}?roleId=${roleId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Person/Role assignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}

//Delete the person/privilege assignment link via the Person Contoller
async function unAssignPrivilegeFromPerson(personId, privilegeId) {
    try {
        // handled in Person Controller
        console.log("personIndex.js:unAssignPrivilegeFromPerson  personId=" & personId & "  privilegeId=" & privilegeId)
        const res = await fetch(`/person/unassignprivilege/${personId}?privilegeId=${privilegeId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Person/Privilege unassignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}

//Delete the person/skill assignment link via the Person Contoller
async function unAssignSkillFromPerson(personId, skillId) {
    try {
        // handled in Person Controller
        console.log("personIndex.js:unAssignSkillFromPerson  personId=" & personId & "  skillId=" & skillId)
        const res = await fetch(`/person/unassignskill/${personId}?skillId=${skillId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Person/Skill unassignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}

//Delete the person/role assignment link via the Person Contoller
async function unAssignRoleFromPerson(personId, roleId) {
    try {
        // handled in Person Controller
        console.log("personIndex.js:unAssignRoleFromPerson  personId=" & personId & "  roleId=" & roleId)
        const res = await fetch(`/person/unassignrole/${personId}?roleId=${roleId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Person/Role unassignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}
