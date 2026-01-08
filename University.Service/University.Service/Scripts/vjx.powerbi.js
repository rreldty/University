function PowerBiEmbeded(accessToken, embedUrl, embedReportId, pageName, showPane, reportSection, reportTable, reportFilter) {

	var paneEnable = true;
	if (showPane !== 1)
		paneEnable = false;

	var filter;
	if (reportTable !== "" && reportFilter !== "") {
		//alert(reportFilter);
		var j = reportFilter.search("ieq");
		if (j > 0) {
			var rFilters = reportFilter.split('ieq');

			if (rFilters.length > 1) {
				var filterVal = [];
				filterVal.push(rFilters[1].trim());

				filter = {
					$schema: "http://powerbi.com/product/schema#basic",
					target: {
						table: reportTable.trim(),
						column: rFilters[0].trim()
					},
					operator: "In",
					values: filterVal
				};
			}
		}

	}

	var models = window['powerbi-client'].models;
	var config = {
		type: 'report',
		tokenType: models.TokenType.Embed,
		accessToken: accessToken,
		embedUrl: embedUrl,
		id: embedReportId,
		permissions: models.Permissions.All,
		pageName: pageName,
		filters: [filter],
		// See https://microsoft.github.io/PowerBI-JavaScript/interfaces/_node_modules_powerbi_models_dist_models_d_.isettings.html
		settings: {
			filterPaneEnabled: false,
			navContentPaneEnabled: paneEnable
			// START Report specific settings
			//layoutType: models.LayoutType.Custom,
			//customLayout: {
			//    displayOption: models.DisplayOption.FitToWidth
			//}
			// END Report specific settings
		}
	};

	var reportContainer = $('#reportContainer')[0];
	var report = powerbi.embed(reportContainer, config);

	console.log(reportSection);

	if (reportSection != null) {
		report.on('loaded', function () {
			report.getPages().then(function (pages) {
				var activePage = pages[reportSection];
				activePage.setActive();
				activePage.setFilters([filter]);
			});
		});
	}
}

function getPbiParam(jFilter, sKey) {
	for (var n = 0; n < jFilter.length; n++) {
		if (jFilter[n].key === sKey)
			return jFilter[n].value;
	}
}

function pbiFullscreen() {
	var reportContainer = $('#reportContainer')[0];

	report = powerbi.get(reportContainer);
	report.fullscreen();
}