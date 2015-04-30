(function () {
    'use strict';
    angular.module('SLBenefitsAppControllers').controller('categoryController', ['$scope', '$http', function ($scope, $http) {
        $scope.title = 'Create Category';

        $scope.categoryList = [];
        $scope.getAll = function () {
            $scope.categoryList = [];
            $http({ method: 'GET', url: 'api/category/getall' }).success(function (response, status) {
                $scope.categoryList = response;
                console.log('inside response ' + response);
            });
        };
        $scope.getAll();
        $scope.postModel = {
            name: ''
        };
        
        $scope.save = function () {
            if (!$scope.postModel.name) {
                noty({ timeout: 10000, layout: 'topRight', type: 'error', text: 'Please enter category name.' });
            } else {
                //save 
                $scope.postModel.isActive = true;
                $http({ method: 'POST', url: 'api/category/save', data: $scope.postModel }).success(function (response, status) {
                    if (response) {
                        $scope.postModel.name = '';
                        console.log('inside response ' + response);
                        $scope.getAll();
                        noty({ timeout: 3000, layout: 'topRight', text: 'category created saved successfully', type: 'success' });
                      }
                });
            }
        };
        //mark end of controller
    }]);
}());