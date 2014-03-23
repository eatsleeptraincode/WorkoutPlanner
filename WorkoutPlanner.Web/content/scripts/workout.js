var app = angular.module('myApp', ['ngGrid']);
app.controller('MyCtrl', function ($scope, $http) {
    var object = $http({ method: 'GET', url: '/workout/grid/e61d1f01-83f5-448b-970e-41fcd07ae12a' });

    $scope.gridOptions = {
            data: 'workoutData',
            enableCellSelection: true,
            enableRowSelection: false,
            enableCellEdit: true,
            columnDefs: [
                { field: 'Group', displayName: 'Group', enableCellEdit: true },
                { field: 'Name', displayName: 'Exercise', enableCellEdit: true },
                { field: 'Sets', displayName: 'Sets', enableCellEdit: true },
                { field: 'Reps', displayName: 'Reps', enableCellEdit: true },
                { field: 'Tempo', displayName: 'Tempo', enableCellEdit: true }
            ]
        };
    object.
    success(function (data, status, headers, config) {
            $scope.workoutData = data;
        }).
    error(function (data, status, headers, config) {
        alert(status);
    });
//    $scope.workoutData = [
//        { Group: "A1", Exercise: "Squat", Sets: 3, Reps: 5, Tempo: "1/0/1/0" },
//        { Group: "A2", Exercise: "SLDL", Sets: 3, Reps: 8, Tempo: "1/1/1/1" },
//        { Group: "B1", Exercise: "Box Jump", Sets: 2, Reps: 12, Tempo: "1/1/1/1" },
//        { Group: "B2", Exercise: "KB Swing", Sets: 2, Reps: 12, Tempo: "1/1/1/1" },
//        { Group: "B3", Exercise: "Jump Rope", Sets: 2, Reps: 15, Tempo: "1/1/1/1" }
//    ];
    
});