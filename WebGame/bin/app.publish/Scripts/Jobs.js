$(document).ready(function () {

        jQuery(".lever-job-link-wrap").clone().appendTo("#new-list ul");

        var options = {
            valueNames: [
                'lever-job-title',
                { data: ['location'] },
                { data: ['department'] },
                { data: ['team'] }
            ]
        };

        var jobList = new List('new-list', options);

        console.log("joblist", jobList);

        var locations = [];
        var teams = [];
        for (var i = 0; i < jobList.items.length; i++) {
            var item = jobList.items[i]._values;
            var location = item.location;
            if (jQuery.inArray(location, locations) == -1) {
                locations.push(location);
            }
            var team = item.team;
            if (jQuery.inArray(team, teams) == -1) {
                teams.push(team);
            }
        }

        locations.sort();
        teams.sort();
        for (var j = 0; j < locations.length; j++) {
            jQuery("#lever-jobs-filter .lever-jobs-filter-locations").append('<option>' + locations[j] + '</option>');
        }
        for (var j = 0; j < teams.length; j++) {
            jQuery("#lever-jobs-filter .lever-jobs-filter-teams").append('<option>' + teams[j] + '</option>');
        }

        function showFilterResults() {
            jQuery('#new-list .list').show();
            jQuery('#lever-jobs-container').hide();
        }
        function hideFilterResults() {
            jQuery('#new-list .list').hide();
            jQuery('#lever-jobs-container').show();
        }

        // Show the unfiltered list by default
        hideFilterResults();

        jQuery('#lever-jobs-filter select').change(function () {

            var selectedFilters = {
                location: jQuery('#lever-jobs-filter select.lever-jobs-filter-locations').val(),
                team: jQuery('#lever-jobs-filter select.lever-jobs-filter-teams').val(),
            }

            //Filter the list
            jobList.filter(function (item) {
                var itemValue = item.values();
                // Check the itemValue against all filter properties (location, team, work-type).
                // ** Note updated to remove work-type from this script -- CF 09/2020 **
                for (var filterProperty in selectedFilters) {
                    var selectedFilterValue = selectedFilters[filterProperty];

                    // For a <select> that has no option selected, JQuery's val() will return null.
                    // We only want to compare properties where the user has selected a filter option,
                    // which is when selectedFilterValue is not null.
                    if (selectedFilterValue !== null) {
                        if (itemValue[filterProperty] !== selectedFilterValue) {
                            // Found mismatch with a selected filter, can immediately exclude this item.
                            return false;
                        }
                    }
                }
                // This item passes all selected filters, include this item.
                return true;
            });

            //Show the 'no results' message if there are no matching results
            if (jobList.visibleItems.length >= 1) {
                jQuery('#lever-no-results').hide();
            }
            else {
                jQuery('#lever-no-results').show();
            }

            console.log("filtered?", jobList.filtered);


            jQuery('#lever-clear-filters').show();

            //Show the list with filtered results
            showFilterResults();

        });


        jQuery('#new-list').on('click', '#lever-clear-filters', function () {
            console.log("clicked clear filters");
            jobList.filter();
            console.log("jobList filtered?", jobList.filtered);
            if (jobList.filtered == false) {
                hideFilterResults();
            }
            jQuery('#lever-jobs-filter select').prop('selectedIndex', 0);
            jQuery('#lever-clear-filters').hide();
            jQuery('#lever-no-results').hide();
        });

        // Showing/hiding search results when the search box is empty
        jQuery('#new-list').on('input', '#lever-jobs-search', function () {
            if (jQuery(this).val().length || jobList.filtered == true) {
                showFilterResults();
                if (jobList.visibleItems.length >= 1) {
                    jQuery('#lever-no-results').hide();
                } else {
                    jQuery('#lever-no-results').show();
                }
            } else {
                hideFilterResults();
                jQuery('#lever-no-results').hide();
            }
        });

    })