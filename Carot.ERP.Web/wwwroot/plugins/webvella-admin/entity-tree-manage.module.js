﻿/* entity-relations.module.js */

/**
* @desc this module manages the entity relations screen in the administration
*/

(function () {
	'use strict';

	angular
        .module('webvellaAdmin') //only gets the module, already initialized in the base.module of the plugin. The lack of dependency [] makes the difference.
        .config(config)
        .controller('CarotAdminManageEntityTreeController', controller)
		.controller('DeleteTreeModalController', deleteTreeModalController);

	// Configuration ///////////////////////////////////
	config.$inject = ['$stateProvider'];


	function config($stateProvider) {
		$stateProvider.state('webvella-admin-entity-tree-manage', {
			parent: 'webvella-admin-base',
			url: '/entities/:entityName/trees/:treeName', //  /desktop/areas after the parent state is prepended
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
					controller: 'CarotAdminManageEntityTreeController',
					templateUrl: '/plugins/webvella-admin/entity-tree-manage.view.html',
					controllerAs: 'ngCtrl'
				}
			},
			resolve: {
				resolvedRelationsList: resolveRelationsList,
				resolvedCurrentEntityRecordTree: resolveCurrentEntityRecordTree,
				translatedFieldTypes: translateFieldTypes,
				resolvedEntityList:resolveEntityList
			},
			data: {

			}
		});
	};


	//#region << Resolve Function >>/////////////////////////

	resolveRelationsList.$inject = ['$q', '$log', 'webvellaCoreService', '$state', '$timeout'];

	function resolveRelationsList($q, $log, webvellaCoreService, $state, $timeout) {
		// Initialize
		var defer = $q.defer();

		// Process
		function successCallback(response) {
			defer.resolve(response.object);
		}

		function errorCallback(response) {
			defer.reject(response.message);
		}

		webvellaCoreService.getRelationsList(successCallback, errorCallback);

		// Return
		return defer.promise;
	}

	resolveCurrentEntityRecordTree.$inject = ['$q', '$log', 'webvellaCoreService', '$state', '$timeout', '$stateParams'];

	function resolveCurrentEntityRecordTree($q, $log, webvellaCoreService, $state, $timeout, $stateParams) {
		// Initialize
		var defer = $q.defer();

		// Process
		function successCallback(response) {
			defer.resolve(response.object);
		}

		function errorCallback(response) {
			defer.reject(response.message);
		}

		webvellaCoreService.getEntityTreeMeta($stateParams.treeName, $stateParams.entityName, successCallback, errorCallback);

		return defer.promise;
	}

	// translateFieldTypes /////////////////////////
	translateFieldTypes.$inject = ['$q', '$log', 'webvellaCoreService'];

	function translateFieldTypes($q, $log, webvellaCoreService) {
		// Initialize
		var defer = $q.defer();

		function successCallback(response) {
			defer.resolve(response);
		}
		webvellaCoreService.getFieldTypes(successCallback);
		return defer.promise;
	}

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

	//#endregion

	// Controller ///////////////////////////////
	controller.$inject = ['$scope', '$sce', '$log', '$rootScope', '$state', '$timeout', 'pageTitle', 'resolvedRelationsList', '$stateParams',
					'$uibModal', 'resolvedCurrentEntityRecordTree', 'webvellaCoreService', 'ngToast', 'translatedFieldTypes', '$translate','resolvedEntityList'];

	function controller($scope, $sce, $log, $rootScope, $state, $timeout, pageTitle, resolvedRelationsList, $stateParams,
					$uibModal, resolvedCurrentEntityRecordTree, webvellaCoreService, ngToast, translatedFieldTypes, $translate, resolvedEntityList) {

		var ngCtrl = this;

		//#region << Init >>
		ngCtrl.search = {};
		ngCtrl.allRelations = resolvedRelationsList;
		ngCtrl.currentEntityRelation = [];
		ngCtrl.entity = webvellaCoreService.getEntityMetaFromEntityList($stateParams.entityName,resolvedEntityList);
		ngCtrl.tree = resolvedCurrentEntityRecordTree;
		//Awesome font icon names array 
		ngCtrl.icons = getFontAwesomeIconNames();
		//#region << Update page title & hide the side menu >>
		$translate(['RECORD_TREE_MANAGE_PAGE_TITLE', 'ENTITIES']).then(function (translations) {
			ngCtrl.pageTitle = translations.RECORD_TREE_MANAGE_PAGE_TITLE + " | " + pageTitle;
			$rootScope.$emit("application-pageTitle-update", ngCtrl.pageTitle);
			$rootScope.adminSectionName = translations.ENTITIES;
		});
		//#endregion        
		$rootScope.adminSubSectionName = ngCtrl.entity.label;
		//#region << Init selected relation >>
		ngCtrl.selectedRelation = null;
		for (var i = 0; i < ngCtrl.allRelations.length; i++) {
			if (ngCtrl.allRelations[i].id == ngCtrl.tree.relationId) {
				ngCtrl.selectedRelation = ngCtrl.allRelations[i];
			}
		}
		//#endregion

		//#region << Node options >>
		ngCtrl.nodeField = {};
		ngCtrl.nodeParentIdField = {};
		ngCtrl.nodeNameField = {};
		ngCtrl.nodeLabelField = {};
		ngCtrl.nodeWeightField = {};
		ngCtrl.nodeNameEligibleFields = [];
		ngCtrl.nodeLabelEligibleFields = [];
		ngCtrl.nodeWeightEligibleFields = [];

		for (var i = 0; i < ngCtrl.entity.fields.length; i++) {
			if (ngCtrl.entity.fields[i].id == ngCtrl.selectedRelation.originFieldId) {
				ngCtrl.nodeIdField = ngCtrl.entity.fields[i];
			}

			if (ngCtrl.entity.fields[i].id == ngCtrl.selectedRelation.targetFieldId) {
				ngCtrl.nodeParentIdField = ngCtrl.entity.fields[i];
			}
			//Fill dictionaries
			switch (ngCtrl.entity.fields[i].fieldType) {
				case 1: //Auto-increment
					if (ngCtrl.entity.fields[i].required) {
						ngCtrl.nodeLabelEligibleFields.push(ngCtrl.entity.fields[i]);
						ngCtrl.nodeNameEligibleFields.push(ngCtrl.entity.fields[i]);
						ngCtrl.nodeWeightEligibleFields.push(ngCtrl.entity.fields[i]);
					}
					break;
				case 12: //Guid
					if (ngCtrl.entity.fields[i].required) {
						ngCtrl.nodeWeightEligibleFields.push(ngCtrl.entity.fields[i]);
					}
					break;
				case 16: //Guid
					if (ngCtrl.entity.fields[i].required) {
						ngCtrl.nodeLabelEligibleFields.push(ngCtrl.entity.fields[i]);
						ngCtrl.nodeNameEligibleFields.push(ngCtrl.entity.fields[i]);
					}
					break;
				case 18: // Text
					if (ngCtrl.entity.fields[i].required) {
						ngCtrl.nodeLabelEligibleFields.push(ngCtrl.entity.fields[i]);
						ngCtrl.nodeNameEligibleFields.push(ngCtrl.entity.fields[i]);
					}
					break;
			}
		}

		ngCtrl.nodeNameEligibleFields = ngCtrl.nodeNameEligibleFields.sort(function (a, b) {
			if (a.name < b.name) return -1;
			if (a.name > b.name) return 1;
			return 0;
		});

		ngCtrl.nodeLabelEligibleFields = ngCtrl.nodeLabelEligibleFields.sort(function (a, b) {
			if (a.name < b.name) return -1;
			if (a.name > b.name) return 1;
			return 0;
		});
		//#endregion

		//#region << nodeNameField >> - auxiliary object
		ngCtrl.nodeNameField = null;
		if (!ngCtrl.tree.nodeNameFieldId) {
			ngCtrl.nodeNameField = ngCtrl.nodeNameEligibleFields[0];
		}
		else {
			for (var i = 0; i < ngCtrl.nodeNameEligibleFields.length; i++) {
				if (ngCtrl.nodeNameEligibleFields[i].id == ngCtrl.tree.nodeNameFieldId) {
					ngCtrl.nodeNameField = ngCtrl.nodeNameEligibleFields[i];
				}
			}
			//if old field id not found in the dictionary, it is probably changed or deleted. 
			if (ngCtrl.nodeNameField == null) {
				ngCtrl.tree.nodeNameFieldId = null;
				ngCtrl.nodeNameField = ngCtrl.nodeNameEligibleFields[0];
			}
		}
		//#endregion

		//#region << nodeLabelField >> - auxiliary object
		ngCtrl.nodeLabelField = null;
		if (!ngCtrl.tree.nodeLabelFieldId) {
			ngCtrl.nodeLabelField = ngCtrl.nodeLabelEligibleFields[0];
		}
		else {
			for (var i = 0; i < ngCtrl.nodeLabelEligibleFields.length; i++) {
				if (ngCtrl.nodeLabelEligibleFields[i].id == ngCtrl.tree.nodeLabelFieldId) {
					ngCtrl.nodeLabelField = ngCtrl.nodeLabelEligibleFields[i];
				}
			}
			//if old field id not found in the dictionary, it is probably changed or deleted. 
			if (ngCtrl.nodeLabelField == null) {
				ngCtrl.tree.nodeLabelFieldId = null;
				ngCtrl.nodeLabelField = ngCtrl.nodeLabelEligibleFields[0];
			}
		}
		//#endregion


		//#region << nodeWeightField >> - auxiliary object
		ngCtrl.nodeWeightField = null;
		if (!ngCtrl.tree.nodeWeightFieldId) {
			//ngCtrl.nodeWeightField = ngCtrl.nodeWeightEligibleFields[0];
		}
		else {
			for (var i = 0; i < ngCtrl.nodeWeightEligibleFields.length; i++) {
				if (ngCtrl.nodeWeightEligibleFields[i].id == ngCtrl.tree.nodeWeightFieldId) {
					ngCtrl.nodeWeightField = ngCtrl.nodeWeightEligibleFields[i];
				}
			}
			//if old field id not found in the dictionary, it is probably changed or deleted. 
			if (ngCtrl.nodeWeightField == null) {
				ngCtrl.tree.nodeWeightFieldId = null;
				ngCtrl.nodeWeightField = ngCtrl.nodeWeightEligibleFields[0];
			}
		}
		//#endregion

		//#endregion

		//#region << Logic >>
		ngCtrl.getRelationBadgeHtml = function (tree) {
			var result = "<span class=\"go-gray\" title=\"Unknown\">?</span>";
			if (ngCtrl.selectedRelation) {
				if (ngCtrl.selectedRelation.relationType == 2) {
					result = "<span title=\"One to Many\">1:n</span>";
				}
				else if (ngCtrl.selectedRelation.relationType == 3) {
					result = '<span title=\"Many to Many\">n:n</span>';
				}
			}
			return $sce.trustAsHtml(result);
		}

		function patchSuccessCallback(response) {
			$translate(['SUCCESS_MESSAGE_LABEL']).then(function (translations) {
				ngToast.create({
					className: 'success',
					content: translations.SUCCESS_MESSAGE_LABEL + " " + response.message
				});
			});
			$timeout(function () {
				ngCtrl.tree = response.object;
				ngCtrl.addRecordId = null;
			}, 0);
		}
		function patchErrorCallback(response) {
			$translate(['ERROR_MESSAGE_LABEL']).then(function (translations) {
				ngToast.create({
					className: 'error',
					content: translations.ERROR_MESSAGE_LABEL + ' ' + response.message,
					timeout: 7000
				});
			});
		}

		ngCtrl.fieldUpdate = function (fieldName, data) {
			var postObj = {};
			postObj[fieldName] = data;
			var index = ngCtrl.tree.nodeObjectProperties.indexOf(data);
			if(index == -1){
				ngCtrl.tree.nodeObjectProperties.push(data);
			}
			postObj.nodeObjectProperties = ngCtrl.tree.nodeObjectProperties;
			webvellaCoreService.patchEntityTree(postObj, ngCtrl.tree.name, ngCtrl.entity.name, patchSuccessCallback, patchErrorCallback)
		}



		//function forceMandatoryFieldsInNodeObjectProperties(){
		//	//check if all the mandatory fields are included
		//	for (var i = 0; i < ngCtrl.entity.fields.length; i++) {
		//		var field = ngCtrl.entity.fields[i];
		//		if(field.name =="weight")
		//		var boz = 0;
		//		var index = ngCtrl.tree.nodeObjectProperties.indexOf(field.id);
		//		if (field.id == ngCtrl.tree.nodeIdFieldId || field.id == ngCtrl.tree.nodeParentIdFieldId ||	field.id == ngCtrl.tree.nodeNameFieldId || field.id == ngCtrl.tree.nodeLabelFieldId || field.id == ngCtrl.tree.nodeWeightFieldId) {
		//			if(index == -1){
		//				ngCtrl.tree.nodeObjectProperties.push(field.id);
		//			}
		//		}
		//	}

		//}

		//forceMandatoryFieldsInNodeObjectProperties();

		ngCtrl.nodeNameUpdate = function (fieldObject) {
			ngCtrl.fieldUpdate('nodeNameFieldId', fieldObject.id);
		}

		ngCtrl.nodeLabelUpdate = function (fieldObject) {
			ngCtrl.fieldUpdate('nodeLabelFieldId', fieldObject.id);
		}

		ngCtrl.nodeWeightUpdate = function (fieldObject) {
			ngCtrl.fieldUpdate('nodeWeightFieldId', fieldObject.id);
		}

		ngCtrl.checkForAddEnter = function (e) {
			var code = (e.keyCode ? e.keyCode : e.which);
			if (code == 13) { //Enter keycode
				ngCtrl.addNewRootNodeById();
			}
		}
		ngCtrl.addNewRootNodeById = function () {
			function successGetRecordCallback(response) {
				var rootNodeObject = {
					"recordId": null,
					"id": null,
					"name": null,
					"label": null,
					"parentId": null
				}
				rootNodeObject.recordId = ngCtrl.addRecordId;
				rootNodeObject.id = response.object[ngCtrl.nodeIdField.name];
				rootNodeObject.parentId = response.object[ngCtrl.nodeParentIdField.name];
				rootNodeObject.name = response.object[ngCtrl.nodeNameField.name];
				rootNodeObject.label = response.object[ngCtrl.nodeLabelField.name];
				var rootNodes = fastCopy(ngCtrl.tree.rootNodes);
				rootNodes.push(rootNodeObject);
				ngCtrl.fieldUpdate('rootNodes', rootNodes);
			}

			function errorGetRecordCallback(response) {
				$translate(['ERROR_MESSAGE_LABEL']).then(function (translations) {
					ngToast.create({
						className: 'error',
						content: translations.ERROR_MESSAGE_LABEL + ' ' + response.message,
						timeout: 7000
					});
				});
				ngCtrl.addRecordId = null;
			}

			webvellaCoreService.getRecord(ngCtrl.addRecordId, "*", ngCtrl.entity.name, successGetRecordCallback, errorGetRecordCallback);
		}

		ngCtrl.removeRootNode = function (record, $index) {
			var rootNodes = fastCopy(ngCtrl.tree.rootNodes);
			rootNodes.splice($index, 1);
			ngCtrl.fieldUpdate('rootNodes', rootNodes);
		}

		ngCtrl.fieldSelectedBy = function (field) {
			//Check if field id is in array
			if(field.name == "weight"){
				var boz = 0;
			}
			var index = ngCtrl.tree.nodeObjectProperties.indexOf(field.id);
			if (index > -1) {
				//Check who selected it
				if (field.id == ngCtrl.tree.nodeIdFieldId || field.id == ngCtrl.tree.nodeParentIdFieldId ||
				field.id == ngCtrl.tree.nodeNameFieldId || field.id == ngCtrl.tree.nodeLabelFieldId || field.id == ngCtrl.tree.nodeWeightFieldId) {
					return "system";
				}
				else {
					return "user";
				}
			}
			else {
				return "none";
			}

		}

		ngCtrl.getFieldType = function (field) {
			var fieldTypes = fastCopy(translatedFieldTypes);
			for (var i = 0; i < fieldTypes.length; i++) {
				if (fieldTypes[i].id == field.fieldType) {
					return fieldTypes[i].label;
				}
			}
			return "";
		}

		ngCtrl.toggleFieldSelection = function (field) {
			//Check if field selected and by who
			switch (ngCtrl.fieldSelectedBy(field)) {
				case "user":
					//We need to remove it from array
					var index = ngCtrl.tree.nodeObjectProperties.indexOf(field.id);
					ngCtrl.tree.nodeObjectProperties.splice(index, 1);
					break;
				case "system":
					break;
				case "none":
					//we need to add it to the array
					ngCtrl.tree.nodeObjectProperties.push(field.id);
					break;
			}
			ngCtrl.fieldUpdate('nodeObjectProperties', ngCtrl.tree.nodeObjectProperties);
		}

		//#endregion

		//#region << Modals >>
		ngCtrl.deleteTreeModal = function () {
			var modalInstance = $uibModal.open({
				animation: false,
				templateUrl: 'deleteTreeModal.html',
				controller: 'DeleteTreeModalController',
				controllerAs: "popupCtrl",
				size: "",
				resolve: {
					parentData: function () { return ngCtrl; }
				}
			});
		}

		//#endregion

	}

	//#region << Modal Controllers >>
	deleteTreeModalController.$inject = ['parentData', '$uibModalInstance', '$log', 'webvellaCoreService', 'ngToast', '$timeout', '$state','$translate'];


	function deleteTreeModalController(parentData, $uibModalInstance, $log, webvellaCoreService, ngToast, $timeout, $state,$translate) {

		var popupCtrl = this;
		popupCtrl.parentData = parentData;

		popupCtrl.ok = function () {

			webvellaCoreService.deleteEntityTree(popupCtrl.parentData.tree.name, popupCtrl.parentData.entity.name, successCallback, errorCallback);

		};

		popupCtrl.cancel = function () {
			$uibModalInstance.dismiss('cancel');
		};

		/// Aux
		function successCallback(response) {
			$translate(['SUCCESS_MESSAGE_LABEL','RECORD_VIEW_SAVE_COPIED_MESSAGE']).then(function (translations) {
				ngToast.create({
					className: 'success',
					content: translations.SUCCESS_MESSAGE_LABEL + " " + response.messageE
				});
			});
			$uibModalInstance.close('success');
			$timeout(function () {
				$state.go("webvella-admin-entity-trees", { entityName: popupCtrl.parentData.entity.name }, { reload: true });
			}, 0);
		}

		function errorCallback(response) {
			popupCtrl.hasError = true;
			popupCtrl.errorMessage = response.message;


		}
	};

	//#endregion
})();


