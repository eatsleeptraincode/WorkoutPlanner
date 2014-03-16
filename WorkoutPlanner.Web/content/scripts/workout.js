var app = angular.module('myApp', ['ngGrid']);
app.controller('MyCtrl', function($scope, $http) {
    $scope.workoutData = [
        {Group: "A1", Exercise: "Squat", Sets: 3, Reps: 5, Tempo: "1/0/1/0"},
        {Group: "A2", Exercise: "SLDL", Sets: 3, Reps: 8, Tempo: "1/1/1/1"},
        {Group: "B1", Exercise: "Box Jump", Sets: 2, Reps: 12, Tempo: "1/1/1/1"},
        {Group: "B2", Exercise: "KB Swing", Sets: 2, Reps: 12, Tempo: "1/1/1/1"},
        {Group: "B3", Exercise: "Jump Rope", Sets: 2, Reps: 15, Tempo: "1/1/1/1"}
    ];
    $scope.gridOptions = { data: 'workoutData' };
});