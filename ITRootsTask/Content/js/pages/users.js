'use strict';

let modelAddTitle = 'Add User';
let modelEditTitle = 'Edit User';
let targetFilterForm = '#UsersFilterForm';


function deleteUser(id) {
    Delete('/User/Delete', id, targetFilterForm);
}

function addUser() {
    LoadPartialInModal(`/User/UserPartial?id=${0}`, modelAddTitle);
}

function editUser(id) {
    LoadPartialInModal(`/User/UserPartial?id=${id}`, modelEditTitle);
}

function successSavingUser(data) {
    Success(data, targetFilterForm);
}
