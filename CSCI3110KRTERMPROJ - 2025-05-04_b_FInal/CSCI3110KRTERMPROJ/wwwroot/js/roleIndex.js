'use strict';

const assignRoleSkillModalDOM = document.getElementById('assignRoleSkillModal');
const assignRoleSkillModal = new bootstrap.Modal(assignRoleSkillModalDOM);

document.querySelectorAll('a.btn-link').forEach(link => {
    link.addEventListener('click', async (e) => {
        e.preventDefault();

        const section = link.getAttribute('data-section');
        const parentTitle = link.getAttribute('data-roletitle');
        //console.log(section);
        console.log("parentTitle=", parentTitle);

        // Get roleId from the href, e.g. /person/assignprivilege/3 where the 3 is the Id
        const urlParts = link.href.split('/');
        const parentId = urlParts[urlParts.length - 1];

        switch (section) {
           case 'roleskill':
                assignRoleSkillModal.show();

                try {
                    const skills = await getAllRoleSkills();
                    populateRoleSkills(skills, parentId, parentTitle);
                } catch (error) {
                    console.error('Error loading skills for a role:', error);
                }
                break;
            default:
                console.error('Error - Unknown data-section from Person or Role Index button link:', error);
        }
    });
});

//Listeners for the CLOSE button on the popup window - closes modal popup and reload page
document.getElementById('btnAssignRoleSkillClose').addEventListener('click', () => {
    assignRoleSkillModal.hide();
    location.reload();    //reload the current webpage so update counts get displayed
});

//Get all items for use in the modal popup from the Controller
async function getAllRoleSkills() {
    try {
        const res = await fetch('/skill/getallforrole');
        const skills = await res.json();
        console.log("roleIndex.js:getAllRoleSkills")
        console.log(skills);
        return skills;
    } catch (err) {
        console.error(err);
    }
}

//Find element id 'roleSkillTableBody' in the partial View class, then populate with the data and event controls
function populateRoleSkills(skills, roleId, roleTitle) {

    //Find modal title element and insert the role title
    const ttitle = document.getElementById('assignRoleSkillModalTitle');
    ttitle.innerHTML = '"' + roleTitle + '"' + ' Skills:';

    //Find the table body element and insert
    const tbody = document.getElementById('roleSkillTableBody');
    tbody.innerHTML = '';

    //build array of skills assigned to person
    const mySkills = [];
    skills.forEach(skill => {
        skill.rolesWithThisSkill.forEach(roleWithThisSkill => {
            const linkRoleId = roleWithThisSkill.roleId;
            if (roleId == linkRoleId) {
                console.log('Skill Id For Role:', skill.skillId);
                mySkills.push(skill.skillId);
            }
        });
    });
    console.log('mySkill Array: ', mySkills);

    //build list of skills to Assign but create an Unassign for those already assigned (skills in the mySkills array) 
    skills.forEach(skill => {
        const row = document.createElement('tr');

        if (mySkills.includes(skill.skillId)) {
            //console.log('skill.rolesWithThisSkill.id: ', id); 
            row.innerHTML = `
                <td>${roleId}</td>
                <td>${skill.skillId}</td>
                <td><b>${skill.skillTitle}</b></td>
                <td><button class="btn btn-warning unassign-btn" data-role-id="${roleId}" data-skill-id="${skill.skillId}">Unassign</button></td>
        `   ;
        } else {
            row.innerHTML = `
                <td>${roleId}</td>
                <td>${skill.skillId}</td>
                <td>${skill.skillTitle}</td>
                <td><button class="btn btn-warning assign-btn" data-role-id="${roleId}" data-skill-id="${skill.skillId}">Assign</button></td>
    `       ;
        };

        tbody.appendChild(row);
    });


    // Hook up the assign buttons listeners
    document.querySelectorAll('.assign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const roleId = btn.dataset.roleId;
            const skillId = btn.dataset.skillId;

            const result = await assignSkillToRole(roleId, skillId);

            if (result === "Ok") {
                alert("Skill successfully assigned to role.");
            } else {
                alert(result.message || "Failed to assign skill to role.");
            }
        });
    });

    // Hook up the unassign buttons listeners
    document.querySelectorAll('.unassign-btn').forEach(btn => {
        btn.addEventListener('click', async () => {
            const roleId = btn.dataset.roleId;
            const skillId = btn.dataset.skillId;

            const result = await unAssignSkillFromRole(roleId, skillId);

            if (result === "Ok") {
                alert("Skill successfully unassigned from role.");
             } else {
                alert(result.message || "Failed to unassign skill from role.");
            }
        });
    });

}

//Insert the assignment link via the Role Contoller
async function assignSkillToRole(roleId, skillId) {
    try {
        /*handled in RoleController*/
        console.log("roleIndex.js:assignSkillToRole  roleId=" & roleId & "  skillId=" & skillId)
        const res = await fetch(`/role/assignskill/${roleId}?skillId=${skillId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Role/Skill assignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}

//Delete the assignment link via the Role Contoller
async function unAssignSkillFromRole(roleId, skillId) {
    try {
        /*handled in RoleController*/
        console.log("roleIndex.js:assignSkillToRole  roleId=" & roleId & "  skillId=" & skillId)
        const res = await fetch(`/role/unassignskill/${roleId}?skillId=${skillId}`);
        const result = await res.json();
        return result;
    } catch (error) {
        console.error('Role/Skill unassignment error in Controller:', error);
        return { message: 'Fetch failed' };
    }
}
