/// <reference path="~/GeneratedArtifacts/viewModel.js" />

myapp.Medlemmer.created = function (screen) {
    // Write code here.
    $.getJSON("../Perms/UserPermissions/", function (data) {

        //attach the permissions to the global 'myapp' object 
        //so it is accessible to the client anywhere.
        myapp.permissions = data;
    });

};