'use strict'

var notesController = function ($scope, $http) {
    $scope.isOnEdit = false;
    $scope.SaveChangesButtonText = "Add Note";
    //data from db
    $scope.notes = "";
    //form models
    $scope.note = { Id: 0, Title: "Set Title", Content: "Anything here...", IsHidden: false };
    $scope.isNoteValid = true;

    //EventHandlers
    $scope.SaveChangesButtonClick = function () {
        if ($scope.isOnEdit === true) {
            $scope.UpdateNote($scope.note);
            $scope.ToggleUpdateMode();
        } else {
            $scope.AddNote($scope.note);
        }

    }

    $scope.UpdateNoteButtonClick = function (id) {
        $scope.GetNoteByID(id);
        $scope.ToggleUpdateMode();
    }


    //Methods
    $scope.GetNotes = function(){
        FetchNotesFromAPI();
    }

    $scope.GetNoteByID = function (id) {
        $http.get("/api/v1/Notes/id/"+id)
        .success(function (response) {
            $scope.note = response;
            console.log(response);
        });
    }

    $scope.AddNote = function (data) {
        $http.post('api/v1/Notes/add', data)
        .success(function (response) {
            console.log( 'add note:' + response);
            //TODO: move to callback
            FetchNotesFromAPI();
        });
    }

    $scope.UpdateNote = function (data) {
        $http.post('api/v1/Notes/update', data)
        .success(function (response) {
            console.log(response);
            //TODO: move to callback
            FetchNotesFromAPI();
        });
    }

    $scope.RemoveNote = function (id) {
        var data = { id: id };

        $http.post('api/v1/Notes/remove/'+ id, data)
        .success(function (response) {
            console.log(response);
            //TODO: move to callback
            FetchNotesFromAPI();
        });
    }

    $scope.ToggleUpdateMode = function () {
        $scope.isOnEdit = !$scope.isOnEdit;

        if ($scope.isOnEdit === true) {
            $scope.SaveChangesButtonText = "Save Changes";
            $scope.isOnEdit = true;
        } else {
            $scope.SaveChangesButtonText = "Add Note";
            $scope.isOnEdit = false;
            $scope.note = { Id: 0, Title: "Set Title", Content: "Anything here...", IsHidden: false };
        }
    }

    //private functions
    function FetchNotesFromAPI() {
        $http.get("/api/v1/Notes/")
        .success(function (response) {
            $scope.notes = response.Data;
            console.log(response);
        });
    }


    //watchers
    $scope.$watch("note", function (newValue, oldValue) {

        if ($scope.note.Title.length > 0 && $scope.note.Title.Content > 0) {
            $scope.isNoteValid = true;
        }
        else {
            $scope.isNoteValid = false;
        }
    });


    //On init
    FetchNotesFromAPI();
}