'use strict'

var notesService = function ($http) {
    var currentHost = window.location.host;

    this.GetAll =function () {
        $http.get(currentHost + "/api/v1/Notes/")
        .success(function (response) {
            return response.Data;
        });
    }

    this.GetByID = function (id) {
        $http.get(currentHost + "/api/v1/Notes/id/" + id)
        .success(function (response) {
            response = response;
        });
    }


}