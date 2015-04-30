(function () {
    'use strict';

    /* App Module */

    var SLBenefitsApp = angular.module('SLBenefitsApp', [
      , 'ui.bootstrap'
      , 'ui.bootstrap.tpls'
      , 'ui.bootstrap.datetimepicker'
      , 'ngRoute'
      , 'ngResource'
      , 'ngSanitize'
      , 'SLBenefitsAppControllers'
      , 'SLBenefitsAppDirectives'
      , 'uiSwitch'
      , 'naif.base64'
    ]);

    angular.module('SLBenefitsAppControllers', []);

    SLBenefitsApp.config(['$routeProvider', '$httpProvider', '$locationProvider', 'datepickerConfig', 'datepickerPopupConfig',
            function ($routeProvider, $httpProvider, $locationProvider, datepickerConfig, datepickerPopupConfig) {

                datepickerConfig.showWeeks = false;
                datepickerConfig.startingDay = 1;
                datepickerPopupConfig.showButtonBar = false;
                datepickerPopupConfig.closeOnDateSelection = true;
                datepickerPopupConfig.popup = 'MM/dd/yyyy';
                //  datepickerPopupConfig.appendToBody = true;

                $httpProvider.interceptors.push('httpResponseInterceptor');
                $httpProvider.defaults.headers.common["X-Requested-With"] = 'XMLHttpRequest';

                $routeProvider.
                    when('/', {
                        templateUrl: 'app/views/dashboard.html',
                        controller: 'dashboardController'
                    })
                    .when('/slbenefits', {
                        templateUrl: 'app/views/dashboard.html',
                        controller: 'dashboardController'
                    })
                     .when('/category/create', {
                         templateUrl: 'App/Views/category.html',
                         controller: 'categoryController'
                     })
                    .otherwise({
                        redirectTo: '/'
                    });
                //  $locationProvider.html5Mode(false).hashPrefix('!');
            }]);

    // Global error handling
    SLBenefitsApp.factory('httpResponseInterceptor', ['$q', '$rootScope', function ($q, $rootScope) {
        var numLoadings = 0;
        return {
            request: function (config) {
                numLoadings++;
                // Show loader
                $rootScope.$broadcast("loader_show");
                return config || $q.when(config);

            },
            response: function (response) {
                // do something on success
                if ((--numLoadings) === 0) {
                    // Hide loader
                    $rootScope.$broadcast("loader_hide");
                }
                return response || $q.when(response);
            },
            responseError: function (response) {
                // Show user friendly error message
                console.log(response);
                if (!(--numLoadings)) {
                    // Hide loader
                    $rootScope.$broadcast("loader_hide");
                }
                if (response.status == 400) {
                    var msg = '';
                    if (response.data != null) {
                        if (response.data.error != null) {
                            if (response.data.error.Message != null && response.data.error.Message != '') {
                                //msg = "Error: " + response.data.error.Message + "<br/><br/>Error occurs when accessing resource <br/>" + response.config.url;
                                msg = "Error: " + response.data.error.Message;
                            }
                        }
                    }
                    if (msg == '')
                        var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Opps! Something went wrong (code:' + response.status + ') when trying access resource <br/><br/>' + response.config.url + '.<br/><br/>We\'ve logged this, but it might work if you try it again.' });
                    else
                        var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: msg });
                } else if (response.status == 401) {
                    var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Unauthorized - Authentication required. Please sign-in to access resource<br/>' + response.config.url });
                } else if (response.status == 403) {
                    var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Forbidden - You don\'t have rights to access the resource<br/>' + response.config.url });
                } else if (response.status == 404) {
                    var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Page Not Found.<br/><br/>The resource <br/>' + response.config.url + '<br/>you are looking for has been removed, had its name changed, or is temporarily unavailable.' });
                } else if (response.status == 500) {
                    //var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Internal server error while trying access resource <br/><br/>' + response.config.url + '<br/><br/>Please try again.' });

                    var msg = '';
                    if (response.data != null && response.data != undefined) {
                        if (response.data.error != null && response.data.error != undefined) {
                            if (response.data.error.Message != null && response.data.error.Message != '') {
                                //msg = "Error: " + response.data.error.Message + "<br/><br/>Error occurs when accessing resource <br/>" + response.config.url;
                                msg = "Error: " + response.data.error.Message;
                            }
                        }
                    }

                    if (msg == '')
                        var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Internal server error. Please try again.' });
                    else
                        var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: msg });
                } else {
                    //var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Opps! Something went wrong (code:' + response.status + ') when trying access resource <br/><br/>' + response.config.url + '.<br/><br/>We\'ve logged this, but it might work if you try it again.' });
                    var n = noty({ timeout: 20000, layout: 'topRight', type: 'error', text: 'Error: Opps! Something went wrong (code:' + response.status + ') when trying access resource. We\'ve logged this, but it might work if you try it again.' });
                }
                return $q.reject(response);
            }
        };
    }]);


}());