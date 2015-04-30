var SLBenefitsAppDirectives = angular.module('SLBenefitsAppDirectives', []);

SLBenefitsAppDirectives.directive("loader", function ($rootScope) {
    return function ($scope, element, attrs) {
        $scope.$on("loader_show", function () {
            return element.show();
        });
        return $scope.$on("loader_hide", function () {
            return element.hide();
        });
    };
});

SLBenefitsAppDirectives.directive('myCustomer', function () {
    return {
        restrict: 'E',
        scope: {
            customerInfo: '=info'
        },
        templateUrl: '/App/Directives/templates/my-customer.html'
    };
});

SLBenefitsAppDirectives.directive('trueFalseQuestion', function () {
    return {
        restrict: 'E',
         scope: {
            directiveQuestionsPostModel: '=info'
        },
        templateUrl: '/App/Directives/templates/true-false-question.html'
    };
});