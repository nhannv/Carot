﻿/* create-record-sidebar.controller.js */

/**
* @desc this controller manages the sidebar of the areas section
*/

(function () {
    'use strict';

    angular
        .module('webvellaAreas')
        .controller('CarotAreasDetachedItemSidebarController', controller);


    // Controller ///////////////////////////////
    controller.$inject = ['$state', '$stateParams','webvellaCoreService','resolvedCurrentUser','resolvedAreas','$location','resolvedEntityList','$localStorage'];

    
    function controller($state, $stateParams,webvellaCoreService,resolvedCurrentUser,resolvedAreas,$location,resolvedEntityList,$localStorage) {
        var sidebarData = this;
		sidebarData.currentArea = webvellaCoreService.getCurrentAreaFromAreaList($stateParams.areaName, resolvedAreas.data);
		sidebarData.currentUser = angular.copy(resolvedCurrentUser);
    	//Generate menu items list
        sidebarData.items = [];
		sidebarData.stateParams = $stateParams;
		sidebarData.sidebarTopActions = [];
		sidebarData.onlyBackButton = true;
		var currentView	= webvellaCoreService.getEntityRecordViewFromEntitiesMetaList($stateParams.viewName, $stateParams.entityName, resolvedEntityList);
		currentView.actionItems.forEach(function (actionItem) {
			switch (actionItem.menu) {
				case "sidebar-top":
					sidebarData.sidebarTopActions.push(actionItem);
					if(actionItem.name != "wv_back_button"){
						sidebarData.onlyBackButton = false;
					}
					break;
			}
		});

		sidebarData.goBack = function(){
			var returnUrl = decodeURI($stateParams.returnUrl);
			$location.search("returnUrl",null);
			$location.path(returnUrl);
		}

        sidebarData.$storage = $localStorage;
        sidebarData.toggleSideNav = function () {
        	sidebarData.$storage.isMiniSidebar = !sidebarData.$storage.isMiniSidebar;
        }
    }

})();
