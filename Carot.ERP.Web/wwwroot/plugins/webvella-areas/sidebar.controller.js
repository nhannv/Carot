﻿/* sidebar.controller.js */

/**
* @desc this controller manages the sidebar of the areas section
*/

(function () {
	'use strict';

	angular
        .module('webvellaAreas') //only gets the module, already initialized in the base.module of the plugin. The lack of dependency [] makes the difference.
        .controller('CarotAreasSidebarController', controller);


	// Controller ///////////////////////////////
	controller.$inject = ['$log', '$rootScope', '$state', '$stateParams', 'resolvedAreas', 'webvellaCoreService', 'resolvedCurrentUser', '$timeout', '$location','$localStorage'];


	function controller($log, $rootScope, $state, $stateParams, resolvedAreas, webvellaCoreService, resolvedCurrentUser, $timeout, $location,$localStorage) {

		var sidebarData = this;
		sidebarData.currentArea = webvellaCoreService.getCurrentAreaFromAreaList($stateParams.areaName, resolvedAreas.data);
		sidebarData.currentArea.attachments = angular.fromJson(sidebarData.currentArea.attachments);
		sidebarData.currentUser = angular.copy(resolvedCurrentUser);
		sidebarData.isCurrentAttachmentActive = function (attachment) {
			if (attachment.url != null) {
				if ($location.path().startsWith(attachment.url)) {
					return true;
				}
				else if ($location.path().startsWith(attachment.url.substr(2))) {	 // try removing the leading "/#"
					return true;
				}
				else {
					return false;
				}
			}
			else if (attachment.name == $stateParams.entityName && ($state.current.name == "webvella-area-list-general" || $state.current.name ==  "webvella-area-view-create")) {
				return true;
			}
			else {
				return false;
			}
		}

        sidebarData.$storage = $localStorage;
        sidebarData.toggleSideNav = function () {
        	sidebarData.$storage.isMiniSidebar = !sidebarData.$storage.isMiniSidebar;
        }
	}

})();
