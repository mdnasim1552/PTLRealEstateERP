
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectSaleMapView.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.ProjectSaleMapView" %>
<!DOCTYPE html>
<html>
<head>
	
	<title>Project via Map</title>

	<meta charset="utf-8" />
	<meta name="viewport" content="width=device-width, initial-scale=1.0">
	
	<!--<link rel="shortcut icon" type="image/x-icon" href="docs/images/favicon.ico" />-->

    <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css" integrity="sha512-xodZBNTC5n17Xt2atTPuE1HxjVMSvLVW9ocqUKLsCC5CXdbqCmblAshOMAS6/keqq/sMZMZ19scR4PsZChSR7A==" crossorigin=""/>
    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js" integrity="sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA==" crossorigin=""></script>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js" type="text/javascript"></script>
	<style>
        .circle{
            float: left;
            height: 15px;
            width: 15px;
            border-radius: 3px;
        }
		html, body {
			height: 100%;
			margin: 0;
		}
		#map {
			width: 100%;
			height: 100%;
		}
	</style>

	
</head>
<body>

<div id='map' ></div>

<script type="text/javascript">


    //var myObj = [
    //     { name: "CS-16306",  procode: "0101001" },
    //     { name: "CS-16308",  procode: "0102001" },
    //     { name: "CS-14081",  procode: "0101001" },
    //     { name: "CS-14606",  procode: "0102002" },
    //     { name: "CS-14610",  procode: "0103001" },
    //     { name: "CS-14075", procode: "0101001" }
    //]
    //var myObj = [
    //     { name: "CS-16306", status: "Purchased" },
    //     { name: "CS-16308", status: "Hand Baina" },
    //     { name: "CS-14081", status: "Discussion" },
    //     { name: "CS-14606", status: "On Registration" },
    //     { name: "CS-14610", status: "Purchased" },
    //     { name: "CS-14075", status: "Purchased" }
    //]
    //console.log(GetLandStatus("CS-16306")); 
    function GetLandStatusWiseColor(name) {
        //console.log(name);
       // console.log(myObj);

        //if (name.search("Road") > 0) {
        if (name.indexOf("Road") > -1) {
            return "yellow";
        }

        var results = [];
        for (var i = 0; i < myObj.length; i++) {
            for (key in myObj[i]) {
                if (myObj[i][key].indexOf(name) != -1) {
                    results.push(myObj[i]);
                }
            }
        }

       // console.log(results);
        if (results.length > 0) {
           // console.log(results[0].procode);
            if (results[0].sstatus == "1") {
                return "red";
            }
            else {
                return "#3388FF";
            }
        }
        else {
            return "#3388FF";
        }
    }

    function getColor(d) {
       
        for (var i = 0; i < purpro.length; i++) {
          
            if (purpro[i].gcode.trim() === d.trim()) {               
                return purpro[i].gdesc2;
            }
           
        }
        //return d === 'Purchased' ? "#f22e1f" :
        //       d === 'Hand Baina' ? "#570c59" :
        //       d === 'On Registration' ? "#4daf4a" :
        //       d === 'Discussion' ? "#0d6327" :
        //                    "#3388FF";
    }

    function style(feature) {
        return {
            weight: 1.5,
            opacity: 1,
            fillOpacity: 1,
            radius: 6,
            fillColor: getColor(feature.properties.TypeOfIssue),
            color: "grey"

        };
    }



    //     function getAreaColor(feature){
    //   console.log(feature)
    // 	switch (feature.properties.Name){
    //   	case 'CS-14075' : return 'blue' ;
    //     case 'CS-14082' : return 'green' ;
    //     	break;
    //   }
    // };

    function areaStyle(feature) {
        //   var status=GetLandStatus(feature.properties.name);
        // console.log(status);
        return {
            //fillColor: getAreaColor(feature),
            // fillColor: feature.properties.fill,
            fillColor: GetLandStatusWiseColor(feature.properties.name),
            weight: 2,
            opacity: 1,
            //  color: 'red',
            //labels:"ssf",
            dashArray: '3',
            fillOpacity: 0.5
        }
    };

    // function polystyle(feature) {
    //     return {
    //         fillColor: 'blue',
    //         weight: 2,
    //         opacity: 1,
    //         color: 'white',  //Outline color
    //         fillOpacity: 0.7
    //     };
    // }

    function clickFeature(e) {
        var layer = e.target;
        map.fitBounds(layer.getBounds());
        // console.log(layer.feature.properties.name); //country info from geojson
    }

    featureByName = {};
    allfeaturelayer = {};
    var i = 0;
    function onEachFeature(feature, layer) {
        var popupContent = "Dag No: " + feature.properties.name +
				 "<br>Description: " + feature.properties.description +
                 "<br>Land Area: " + feature.properties.description +
                 "<br>Khatian No: " + feature.properties.description +
                 "<br>Land Owner: " + feature.properties.description;

        if (feature.properties && feature.properties.popupContent) {
            popupContent += feature.properties.popupContent;
        }
        // for click zooming on dag area
        // layer.on({
        //         click: clickFeature
        //     });
        allfeaturelayer[feature.properties.name] = layer;
        featureByName[i++] = feature.properties.name;
        layer.bindTooltip(feature.properties.name);
        layer.bindPopup(popupContent);

    }

    // dag list







    function DagWiseZoom(e) {
        //  alert(e);
        //populationLegend.remove();
        $("div").remove(".details");

        //add single land data info
        var results = [];
        for (var i = 0; i < myObj.length; i++) {
            for (key in myObj[i]) {
                if (myObj[i][key].indexOf(e) != -1) {
                    results.push(myObj[i]);
                }
            }
        }

        // console.log(results);
     


            var DetailsLegend = L.control({ position: 'topleft' });


            DetailsLegend.onAdd = function (map) {
                var div = L.DomUtil.create('div', 'details legend');
                if (results.length > 0) {
                div.innerHTML +=
                '<table style="background: rgba(223, 230, 172,0.2); padding:5px;">' +
                '<tr><td colspan="2"><b>Details Information<b></td></tr>' +
                '<tr><td>Project</td><td>:' + results[0].pactdesc + '</td></tr>' +
                 '<tr><td>Title</td><td>:' + results[0].title + '</td></tr>' +
                '<tr><td>Unit</td><td>:' + results[0].munit + '</td></tr>' +
                '<tr><td>Size</td><td>:' + results[0].usize + '</td></tr>' +
                '<tr><td>Budget Price</td><td>:' + results[0].uamt + '</td></tr>' +
                '<tr><td>Sale Amt</td><td>:' + results[0].saleamt + '</td></tr>' +
                      '</table>';
                }
                else {
                    div.innerHTML += "<p style='color:red; font-size:14px;'>No Details Data Found</p>";
                }
                return div;
            };


            // Add this one (only) for now, as the Population layer is on by default
            DetailsLegend.addTo(map);

        


        /// zoom specific land by clicking dag no
        var zoomset = allfeaturelayer[e]._bounds;
       //console.log(allfeaturelayer[e]);
       // console.log(allfeaturelayer[e]._latlngs);
        var center = allfeaturelayer[e].getBounds().getCenter();
        L.marker(center).addTo(map).bindPopup("Dag No:" + e);

        map.fitBounds(zoomset);
    }

    var map = L.map('map').setView([23.834479443790272, 90.46955918228787], 20);
    var purpro = "";
    var myObj = "";
     GetPurProcess();
    
     function GetPurProcess() {
        $.ajax({
            type: "POST",
            url: "ProjectSaleMapView.aspx/GetLandInfo",
          //  data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
              //  Show(); // Show loader icon  
            },
            success: function (response) {
               // console.log(response.d);
                 console.log(JSON.parse(response.d));               
                 myObj = JSON.parse(response.d);
                MapGenerate();
                //  console.log(purprocess[0])
                //return JSON.parse(response.d);
                // Looping over emloyee list and display it  
                //$.each(response, function (index, emp) {
                //    $('#output').append('<p>Id: ' + emp.ID + '</p>' +
                //                        '<p>Id: ' + emp.Name + '</p>');
                //});
            },
            complete: function () {
              //  Hide(); // Hide loader icon  
            },
            failure: function (jqXHR, textStatus, errorThrown) {
                alert("HTTP Status: " + jqXHR.status + "; Error Text: " + jqXHR.responseText); // Display error message  
            }
        });
    }
  
     function MapGenerate() {
        
         //console.log(purprocess);

         var freeBus = {
             "type": "FeatureCollection",
             "features": [
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4695708,
                             23.8347147,
                             0
                           ],
                           [
                             90.4695761,
                             23.8346969,
                             0
                           ],
                           [
                             90.4695895,
                             23.8345092,
                             0
                           ],
                           [
                             90.4695453,
                             23.8344687,
                             0
                           ],
                           [
                             90.4693401,
                             23.8344589,
                             0
                           ],
                           [
                             90.4693414,
                             23.834416,
                             0
                           ],
                           [
                             90.4695466,
                             23.8344209,
                             0
                           ],
                           [
                             90.4696083,
                             23.8343865,
                             0
                           ],
                           [
                             90.4696432,
                             23.8341743,
                             0
                           ],
                           [
                             90.469704,
                             23.834187,
                             0
                           ],
                           [
                             90.469666,
                             23.8343902,
                             0
                           ],
                           [
                             90.469705,
                             23.83444,
                             0
                           ],
                           [
                             90.4700576,
                             23.8345276,
                             0
                           ],
                           [
                             90.4700415,
                             23.8345693,
                             0
                           ],
                           [
                             90.4696942,
                             23.8344847,
                             0
                           ],
                           [
                             90.4696405,
                             23.8345239,
                             0
                           ],
                           [
                             90.4696204,
                             23.8347141,
                             0
                           ],
                           [
                             90.4695708,
                             23.8347147,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Main Road"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4693508,
                             23.8344148,
                             0
                           ],
                           [
                             90.4692677,
                             23.8342933,
                             0
                           ],
                           [
                             90.4694433,
                             23.8342982,
                             0
                           ],
                           [
                             90.4694353,
                             23.8344172,
                             0
                           ],
                           [
                             90.4693508,
                             23.8344148,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 5"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694641,
                             23.8344197,
                             0
                           ],
                           [
                             90.4694728,
                             23.8343022,
                             0
                           ],
                           [
                             90.4696184,
                             23.8343074,
                             0
                           ],
                           [
                             90.4696083,
                             23.8343865,
                             0
                           ],
                           [
                             90.4695466,
                             23.8344209,
                             0
                           ],
                           [
                             90.4694641,
                             23.8344197,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 6"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4692677,
                             23.8342933,
                             0
                           ],
                           [
                             90.4692684,
                             23.8342694,
                             0
                           ],
                           [
                             90.4696224,
                             23.8342804,
                             0
                           ],
                           [
                             90.4696184,
                             23.8343074,
                             0
                           ],
                           [
                             90.4692677,
                             23.8342933,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Road 1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4692684,
                             23.8342694,
                             0
                           ],
                           [
                             90.469268,
                             23.834174,
                             0
                           ],
                           [
                             90.4694273,
                             23.8341743,
                             0
                           ],
                           [
                             90.4694165,
                             23.8342737,
                             0
                           ],
                           [
                             90.4692684,
                             23.8342694,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 7"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694165,
                             23.8342737,
                             0
                           ],
                           [
                             90.4694273,
                             23.8341743,
                             0
                           ],
                           [
                             90.4694554,
                             23.8341792,
                             0
                           ],
                           [
                             90.4694453,
                             23.8342752,
                             0
                           ],
                           [
                             90.4694165,
                             23.8342737,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Road 1/1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694453,
                             23.8342752,
                             0
                           ],
                           [
                             90.4694554,
                             23.8341792,
                             0
                           ],
                           [
                             90.4694675,
                             23.8341817,
                             0
                           ],
                           [
                             90.4694796,
                             23.8341387,
                             0
                           ],
                           [
                             90.469635,
                             23.83417,
                             0
                           ],
                           [
                             90.4696432,
                             23.8341743,
                             0
                           ],
                           [
                             90.4696224,
                             23.8342804,
                             0
                           ],
                           [
                             90.4694453,
                             23.8342752,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "L # A, RD # 1 PLOT # 8"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694353,
                             23.8344172,
                             0
                           ],
                           [
                             90.469443,
                             23.8343005,
                             0
                           ],
                           [
                             90.4694728,
                             23.8343022,
                             0
                           ],
                           [
                             90.4694641,
                             23.8344197,
                             0
                           ],
                           [
                             90.4694353,
                             23.8344172,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Road 1/2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469252,
                             23.834621,
                             0
                           ],
                           [
                             90.469248,
                             23.834592,
                             0
                           ],
                           [
                             90.4695828,
                             23.8345896,
                             0
                           ],
                           [
                             90.4695788,
                             23.834624,
                             0
                           ],
                           [
                             90.469252,
                             23.834621,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Block A, Road 1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694102,
                             23.834592,
                             0
                           ],
                           [
                             90.4694122,
                             23.8344632,
                             0
                           ],
                           [
                             90.469435,
                             23.8344644,
                             0
                           ],
                           [
                             90.4694316,
                             23.834592,
                             0
                           ],
                           [
                             90.4694102,
                             23.834592,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Block A, Road 3"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694074,
                             23.8346202,
                             0
                           ],
                           [
                             90.4694296,
                             23.8346214,
                             0
                           ],
                           [
                             90.4694309,
                             23.834722,
                             0
                           ],
                           [
                             90.4694095,
                             23.8347257,
                             0
                           ],
                           [
                             90.4694074,
                             23.8346202,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Block A, Road 2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469252,
                             23.834621,
                             0
                           ],
                           [
                             90.4693243,
                             23.83462,
                             0
                           ],
                           [
                             90.4694074,
                             23.8346202,
                             0
                           ],
                           [
                             90.4694095,
                             23.8347257,
                             0
                           ],
                           [
                             90.469258,
                             23.834725,
                             0
                           ],
                           [
                             90.46926,
                             23.8347,
                             0
                           ],
                           [
                             90.469255,
                             23.83468,
                             0
                           ],
                           [
                             90.469252,
                             23.834621,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694309,
                             23.834722,
                             0
                           ],
                           [
                             90.4694296,
                             23.8346214,
                             0
                           ],
                           [
                             90.4695788,
                             23.834624,
                             0
                           ],
                           [
                             90.4695761,
                             23.8346969,
                             0
                           ],
                           [
                             90.4695708,
                             23.8347147,
                             0
                           ],
                           [
                             90.469455,
                             23.834718,
                             0
                           ],
                           [
                             90.4694309,
                             23.834722,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694316,
                             23.834592,
                             0
                           ],
                           [
                             90.469435,
                             23.8344644,
                             0
                           ],
                           [
                             90.4695453,
                             23.8344687,
                             0
                           ],
                           [
                             90.4695895,
                             23.8345092,
                             0
                           ],
                           [
                             90.4695828,
                             23.8345896,
                             0
                           ],
                           [
                             90.4694316,
                             23.834592,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 4"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694102,
                             23.834592,
                             0
                           ],
                           [
                             90.469248,
                             23.834592,
                             0
                           ],
                           [
                             90.469242,
                             23.834564,
                             0
                           ],
                           [
                             90.469258,
                             23.834501,
                             0
                           ],
                           [
                             90.469277,
                             23.834491,
                             0
                           ],
                           [
                             90.469332,
                             23.834496,
                             0
                           ],
                           [
                             90.4693401,
                             23.8344589,
                             0
                           ],
                           [
                             90.4694122,
                             23.8344632,
                             0
                           ],
                           [
                             90.4694102,
                             23.834592,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 1 PLOT # 3"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469782,
                             23.834726,
                             0
                           ],
                           [
                             90.4698241,
                             23.834518,
                             0
                           ],
                           [
                             90.469867,
                             23.8345278,
                             0
                           ],
                           [
                             90.4698133,
                             23.8347364,
                             0
                           ],
                           [
                             90.469782,
                             23.834726,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Block B, Road 1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696806,
                             23.8343191,
                             0
                           ],
                           [
                             90.4696846,
                             23.8342971,
                             0
                           ],
                           [
                             90.4699595,
                             23.8343486,
                             0
                           ],
                           [
                             90.4699582,
                             23.8343707,
                             0
                           ],
                           [
                             90.4696806,
                             23.8343191,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Road 4"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696402,
                             23.8345239,
                             0
                           ],
                           [
                             90.4696939,
                             23.8344847,
                             0
                           ],
                           [
                             90.4698238,
                             23.834518,
                             0
                           ],
                           [
                             90.4698013,
                             23.8346258,
                             0
                           ],
                           [
                             90.4696306,
                             23.8345915,
                             0
                           ],
                           [
                             90.4696402,
                             23.8345239,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # A, RD # 2 PLOT # 2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696319,
                             23.834616,
                             0
                           ],
                           [
                             90.4696343,
                             23.8345924,
                             0
                           ],
                           [
                             90.4698013,
                             23.8346258,
                             0
                           ],
                           [
                             90.4697963,
                             23.8346516,
                             0
                           ],
                           [
                             90.4696319,
                             23.834616,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Block B, Road 2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696846,
                             23.8342971,
                             0
                           ],
                           [
                             90.4696933,
                             23.8342437,
                             0
                           ],
                           [
                             90.4698093,
                             23.8342284,
                             0
                           ],
                           [
                             90.469946,
                             23.834245,
                             0
                           ],
                           [
                             90.4699407,
                             23.8342995,
                             0
                           ],
                           [
                             90.469965,
                             23.83433,
                             0
                           ],
                           [
                             90.4699595,
                             23.8343486,
                             0
                           ],
                           [
                             90.4696846,
                             23.8342971,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # C ROAD # 02 PLOT #1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469666,
                             23.8343902,
                             0
                           ],
                           [
                             90.4696806,
                             23.8343191,
                             0
                           ],
                           [
                             90.4698294,
                             23.8343474,
                             0
                           ],
                           [
                             90.469809,
                             23.834465,
                             0
                           ],
                           [
                             90.469705,
                             23.83444,
                             0
                           ],
                           [
                             90.469666,
                             23.8343902,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # C ROAD # 01 PLOT #1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469809,
                             23.834465,
                             0
                           ],
                           [
                             90.4698294,
                             23.8343474,
                             0
                           ],
                           [
                             90.4699582,
                             23.8343707,
                             0
                           ],
                           [
                             90.4699471,
                             23.8344367,
                             0
                           ],
                           [
                             90.469944,
                             23.8344989,
                             0
                           ],
                           [
                             90.469896,
                             23.8344861,
                             0
                           ],
                           [
                             90.4698587,
                             23.8344783,
                             0
                           ],
                           [
                             90.469809,
                             23.834465,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # C ROAD # 01 PLOT #2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469846,
                             23.8346199,
                             0
                           ],
                           [
                             90.469867,
                             23.8345278,
                             0
                           ],
                           [
                             90.4700415,
                             23.8345693,
                             0
                           ],
                           [
                             90.470011,
                             23.834654,
                             0
                           ],
                           [
                             90.4699291,
                             23.8346346,
                             0
                           ],
                           [
                             90.469846,
                             23.8346199,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # B ROAD # 02 PLOT #1"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469369,
                             23.834168,
                             0
                           ],
                           [
                             90.469357,
                             23.834118,
                             0
                           ],
                           [
                             90.4694796,
                             23.8341387,
                             0
                           ],
                           [
                             90.4694675,
                             23.8341817,
                             0
                           ],
                           [
                             90.469369,
                             23.834168,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "Plot 12"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4698133,
                             23.8347364,
                             0
                           ],
                           [
                             90.469846,
                             23.8346199,
                             0
                           ],
                           [
                             90.4699202,
                             23.834634,
                             0
                           ],
                           [
                             90.470011,
                             23.834654,
                             0
                           ],
                           [
                             90.4699752,
                             23.8347303,
                             0
                           ],
                           [
                             90.469962,
                             23.83476,
                             0
                           ],
                           [
                             90.46994,
                             23.834753,
                             0
                           ],
                           [
                             90.46985,
                             23.834727,
                             0
                           ],
                           [
                             90.4698133,
                             23.8347364,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # B ROAD # 01 PLOT #2"
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696319,
                             23.834616,
                             0
                           ],
                           [
                             90.4697963,
                             23.8346516,
                             0
                           ],
                           [
                             90.469782,
                             23.834726,
                             0
                           ],
                           [
                             90.4697,
                             23.83469,
                             0
                           ],
                           [
                             90.469695,
                             23.834712,
                             0
                           ],
                           [
                             90.4696204,
                             23.8347141,
                             0
                           ],
                           [
                             90.4696319,
                             23.834616,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "BL # B ROAD # 01 PLOT #1"
                   }
               }
             ]
         }





        

         L.tileLayer('https://api.mapbox.com/styles/v1/{id}/tiles/{z}/{x}/{y}?access_token=pk.eyJ1IjoibWFwYm94IiwiYSI6ImNpejY4NXVycTA2emYycXBndHRqcmZ3N3gifQ.rJcFIG214AriISLbB6B5aw', {
             maxZoom: 22,
             attribution: 'Map data &copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors, ' +
                 'Imagery © <a href="https://www.mapbox.com/">Mapbox</a>',
             id: 'mapbox/light-v9',
             tileSize: 512,
             zoomOffset: -1


         }).addTo(map);

         var baseballIcon = L.icon({
             iconUrl: 'baseball-marker.png',
             iconSize: [32, 37],
             iconAnchor: [16, 37],
             popupAnchor: [0, -28]
         });
         ////// dag wise color


        


         var legend = L.control({ position: 'bottomleft' });
         legend.onAdd = function (map) {

             var div = L.DomUtil.create('div', 'info legend');
             labels = ['<strong>Sale Status</strong>'],
        
         colors = ['red', '#3388FF','yellow'];
             categories = ['Sold', 'Un-Sold', 'Non-saleable'];

             for (var i = 0; i < categories.length; i++) {

                 div.innerHTML +=
                 labels.push(
                     '<i class="circle" style="background-color:' + colors[i]+'"></i> ' +
                 (categories[i] ? categories[i] : '+'));

             }
             div.innerHTML = labels.join('<br>');
             return div;
         };
         legend.addTo(map);


         L.geoJSON(freeBus, {

             filter: function (feature, layer) {
                 if (feature.properties) {
                     // If the property "underConstruction" exists and is true, return false (don't render features under construction)
                     return feature.properties.underConstruction !== undefined ? !feature.properties.underConstruction : true;
                 }

                 return false;
             },

             style: areaStyle,
             onEachFeature: onEachFeature
         }).addTo(map);
         //console.log(featureByName[0]);
         //console.log(map._layers);
         // console.log(featureByName[0]);
         var flength = featureByName.length;
         // console.log(flength);


         var legendDag = L.control({ position: 'topright' });
         legendDag.onAdd = function (map) {

             var div = L.DomUtil.create('div', 'info legend');
             labels = ['<strong>PLOT LIST</strong>'];


             // console.log(featureByName);
             var dlength = 0;

             for (var key in featureByName) {
                 if (featureByName.hasOwnProperty(key)) {
                     div.innerHTML +=
                       labels.push(
                           "<i class='circle' style='background-color:" + GetLandStatusWiseColor(featureByName[dlength]) + "'></i> &nbsp; <a href='javascript: void(0)' style='text-decoration:none;' id='" + featureByName[dlength] + "' onclick='DagWiseZoom(this.id)'>" +
                       (featureByName[dlength] ? featureByName[dlength] : '</a>+'));
                     dlength++;
                 }
             }
             div.innerHTML = labels.join('<br>');
             return div;
         };
         legendDag.addTo(map);

     }
    


   
</script>



</body>
</html>

