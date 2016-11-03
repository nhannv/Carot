﻿/* entity-views.module.js */

/**
* @desc this module manages the entity views in the admin screen
*/

(function () {
	'use strict';

	angular
        .module('webvellaAdmin') //only gets the module, already initialized in the base.module of the plugin. The lack of dependency [] makes the difference.
        .config(config)
        .controller('CarotAdminEntityViewsController', controller)
	    .controller('CreateViewModalController', createViewModalController)
		.controller('CopyViewModalController', CopyViewModalController);

	// Configuration ///////////////////////////////////
	config.$inject = ['$stateProvider'];

	
	function config($stateProvider) {
		$stateProvider.state('webvella-admin-entity-views', {
			parent: 'webvella-admin-base',
			url: '/entities/:entityName/views', //  /desktop/areas after the parent state is prepended
			views: {
				"topnavView": {
					controller: 'CarotAdminTopnavController',
					templateUrl: '/plugins/webvella-admin/topnav.view.html',
					controllerAs: 'topnavData'
				},
				"sidebarView": {
					controller: 'CarotAdminSidebarController',
					templateUrl: '/plugins/webvella-admin/sidebar-avatar-only.view.html',
					controllerAs: 'sidebarData'
				},
				"contentView": {
					controller: 'CarotAdminEntityViewsController',
					templateUrl: '/plugins/webvella-admin/entity-views.view.html',
					controllerAs: 'ngCtrl'
				}
			},
			resolve: {
				resolvedEntityList:resolveEntityList
			},
			data: {

			}
		});
	};


	// Resolve Function /////////////////////////
	resolveEntityList.$inject = ['$q', '$log', 'webvellaCoreService', '$state', '$stateParams'];
	function resolveEntityList($q, $log, webvellaCoreService, $state, $stateParams) {
		var defer = $q.defer();
		function successCallback(response) {
			defer.resolve(response.object);
		}
		function errorCallback(response) {
			defer.reject(response.message);
		}
		webvellaCoreService.getEntityMetaList(successCallback, errorCallback);
		return defer.promise;
	}


	// Controller ///////////////////////////////
	controller.$inject = ['$scope', '$log', '$rootScope', '$state', 'pageTitle', 'resolvedEntityList', '$uibModal', '$timeout','$translate','$stateParams','webvellaCoreService'];

	
	function controller($scope, $log, $rootScope, $state, pageTitle, resolvedEntityList, $uibModal, $timeout,$translate,$stateParams, webvellaCoreService) {
		
		var ngCtrl = this;
		ngCtrl.entity = webvellaCoreService.getEntityMetaFromEntityList($stateParams.entityName,resolvedEntityList);
		ngCtrl.views = ngCtrl.entity.recordViews;
		if (ngCtrl.views === null) {
			ngCtrl.views = [];
		}
		ngCtrl.views.sort(function (a, b) {
			if (a.name < b.name) return -1;
			if (a.name > b.name) return 1;
			return 0;
		});

		//#region << Update page title & hide the side menu >>
		$translate(['RECORD_VIEW_LIST_PAGE_TITLE', 'ENTITIES']).then(function (translations) {
			ngCtrl.pageTitle = translations.RECORD_VIEW_LIST_PAGE_TITLE + " | " + pageTitle;
			$rootScope.$emit("application-pageTitle-update", ngCtrl.pageTitle);
			$rootScope.adminSectionName = translations.ENTITIES;
		});
		$rootScope.adminSubSectionName = ngCtrl.entity.label;
    	//#endregion

		ngCtrl.calculateStats = function (view) {
			var itemsCount = 0;
			var sectionsCount = 0;
			for (var i = 0; i < view.regions.length; i++) {
				sectionsCount += view.regions[i].sections.length;
				var sections = view.regions[i].sections;
				for (var j = 0; j < sections.length; j++) {
					var rows = sections[j].rows;
					for (var m = 0; m < rows.length; m++) {
						var columns = rows[m].columns;
						for (var k = 0; k < columns.length; k++) {
							var items = columns[k].items;
							itemsCount += items.length;
						}
					}
				}
			}


			if (sectionsCount != 0) {
				return "<span class='go-green'>" + itemsCount + "</span> items and <span class='go-green'>" + sectionsCount + "</span>  sections";
			}
			else {
				return "<span class='go-gray'>empty</span>";
			}
		}


		//Create new view modal
		ngCtrl.createView = function () {

			var modalInstance = $uibModal.open({
				animation: false,
				templateUrl: 'createViewModal.html',
				controller: 'CreateViewModalController',
				controllerAs: "popupCtrl",
				size: "",
				resolve: {
					ngCtrl: function () {
						return ngCtrl;
					}
				}
			});

		}

		//Cppy new view modal
		ngCtrl.copyView = function (view) {

			var modalInstance = $uibModal.open({
				animation: false,
				templateUrl: 'copyModal.html',
				controller: 'CopyViewModalController',
				controllerAs: "popupCtrl",
				size: "",
				resolve: {
					ngCtrl: function () {
						return ngCtrl;
					},
					view: function () {
						return view;
					}
				}
			});

		}
	}


	//// Modal Controllers
	createViewModalController.$inject = ['$uibModalInstance', '$log', 'ngToast', '$timeout', '$state', '$location', 'ngCtrl', 'webvellaCoreService'];
	
	function createViewModalController($uibModalInstance, $log, ngToast, $timeout, $state, $location, ngCtrl, webvellaCoreService) {
		
		var popupCtrl = this;
		popupCtrl.modalInstance = $uibModalInstance;
		popupCtrl.validation = {};
		popupCtrl.validation.hasError = false;
		popupCtrl.validation.errorMessage = false;
		popupCtrl.view = webvellaCoreService.initView("general");
		popupCtrl.currentEntity = fastCopy(ngCtrl.entity);
		popupCtrl.existingViews = fastCopy(ngCtrl.views);
		popupCtrl.view.default = true;
        popupCtrl.viewTypes = [
		{
			name: "general",
			label: "general"
		},
		{
			name: "quick_view",
			label: "quick view"
		},
		{
			name: "create",
			label: "create"
		},
		{
			name: "quick_create",
			label: "quick create"
		},
		{
			name: "hidden",
			label: "hidden"
		}
        ];

		popupCtrl.regenActionItems = function(){
			var templateView = webvellaCoreService.initView(popupCtrl.view.type);
			popupCtrl.view.actionItems = templateView.actionItems;
		}

		popupCtrl.ok = function () {
			//Validate if there is not already a view with this name
			popupCtrl.validation = {};
			popupCtrl.validation.hasError = false;
			popupCtrl.validation.errorMessage = false;
			for (var i = 0; i < popupCtrl.existingViews.length; i++) {
				if(popupCtrl.existingViews[i].name == popupCtrl.view.name){
					popupCtrl.validation.hasError = true;
					popupCtrl.validation.errorMessage = "This view name is already used";
				}
			}
			if(!popupCtrl.validation.hasError){
				webvellaCoreService.createEntityView(popupCtrl.view, popupCtrl.currentEntity.name, successCallback, errorCallback);
			}
		};

		popupCtrl.cancel = function () {
			$uibModalInstance.dismiss('cancel');
		};

		/// Aux
		function successCallback(response) {
			ngToast.create({
				className: 'success',
				content: '<span class="go-green">Success:</span> ' + 'The view was successfully saved'
			});
			$uibModalInstance.close('success');
			webvellaCoreService.GoToState($state.current.name, {});
		}

		function errorCallback(response) {
			var location = $location;
			//Process the response and generate the validation Messages
			webvellaCoreService.generateValidationMessages(response, popupCtrl, popupCtrl.entity, location);
		}
	};


	CopyViewModalController.$inject = ['$uibModalInstance', '$log', 'ngToast', '$timeout', '$state', '$location', 'ngCtrl', 'view', 'webvellaCoreService'];
	
	function CopyViewModalController($uibModalInstance, $log, ngToast, $timeout, $state, $location, ngCtrl, view, webvellaCoreService) {
		
		var popupCtrl = this;
		popupCtrl.modalInstance = $uibModalInstance;
		popupCtrl.view = fastCopy(view);
		popupCtrl.currentEntity = fastCopy(ngCtrl.entity);
		popupCtrl.alternative = "new";
		popupCtrl.viewName = null;
		popupCtrl.viewNameValidationError = false;

		popupCtrl.entityViews = []; //filter the current view

		for (var i = 0; i < popupCtrl.currentEntity.recordViews.length; i++) {
			if (popupCtrl.currentEntity.recordViews[i].name != popupCtrl.view.name) {
				popupCtrl.entityViews.push(popupCtrl.currentEntity.recordViews[i]);
			}
		}

		popupCtrl.selectedView = popupCtrl.entityViews[0];

		popupCtrl.ok = function () {
			popupCtrl.viewNameValidationError = false;
			if (popupCtrl.alternative == "new") {
				if (popupCtrl.viewName == null || popupCtrl.viewName == "") {
					popupCtrl.viewNameValidationError = true;
				}
				else {
					var newView = fastCopy(popupCtrl.view);
					newView.id = null;
					newView.name = popupCtrl.viewName;
					newView.label = popupCtrl.viewName;
					webvellaCoreService.createEntityView(newView, popupCtrl.currentEntity.name, successCallback, errorCallback);
				}
			}
			else {
				var newView = fastCopy(popupCtrl.view);
				var oldView = fastCopy(popupCtrl.selectedView);
				oldView.regions = newView.regions;
				oldView.sidebar = newView.sidebar;
				oldView.relationOptions = newView.relationOptions;
				webvellaCoreService.updateEntityView(oldView, popupCtrl.currentEntity.name, successCallback, errorCallback);
			}
		};

		popupCtrl.cancel = function () {
			$uibModalInstance.dismiss('cancel');
		};

		/// Aux
		function successCallback(response) {
			ngToast.create({
				className: 'success',
				content: '<span class="go-green">Success:</span> ' + 'The view was successfully saved'
			});
			$uibModalInstance.close('success');
			webvellaCoreService.GoToState($state.current.name, {});
		}

		function errorCallback(response) {
			ngToast.create({
				className: 'danger',
				content: '<span class="go-red">Error:</span> ' + response.message
			});
		}
	};
 })();


