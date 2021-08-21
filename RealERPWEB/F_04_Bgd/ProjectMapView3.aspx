<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectMapView3.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.ProjectMapView3" %>


<!DOCTYPE html>
<html>
<head>
	
	<title>Jamuna Project</title>

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
       // console.log(myObj);
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
            return getColor(results[0].procode);
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
                '<tr><td>Name</td><td>:' + results[0].ownername + '</td></tr>' +
                '<tr><td>Father</td><td>:' + results[0].fathername + '</td></tr>' +
                '<tr><td>Mother</td><td>:' + results[0].mothername + '</td></tr>' +
                '<tr><td>Address</td><td>:' + results[0].owaddress + '</td></tr>' +
                '<tr><td>Contact</td><td>:' + results[0].contactno + '</td></tr>' +
                '<tr><td>Dalil No</td><td>:' + results[0].dalilno + '</td></tr>' +
                '<tr><td>CS Dag</td><td>:' + results[0].csdag + '</td></tr>' +
                '<tr><td>CS Khatin</td><td>:' + results[0].cskhatian + '</td></tr>' +
                '<tr><td>Land Type</td><td>:' + results[0].landtype + '</td></tr>' +
                '<tr><td>Total Land</td><td>:' + results[0].landarea + '(Dec)</td></tr>' +
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
            url: "ProjectMapView3.aspx/GetLandInfo",
          //  data: {},
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            beforeSend: function () {
              //  Show(); // Show loader icon  
            },
            success: function (response) {
               // console.log(response.d);
                 console.log(JSON.parse(response.d));
                 purpro = JSON.parse(response.d).prodata;
                 myObj = JSON.parse(response.d).landinfo;
                MapGenerate(JSON.parse(response.d).prodata);
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
  
     function MapGenerate(purprocess) {
        
         //console.log(purprocess);
         var freeBus = {
             "type": "FeatureCollection",
             "features": [
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46945,
                             23.8342075,
                             0.0
                           ],
                           [
                             90.4694443,
                             23.8341274,
                             0.0
                           ],
                           [
                             90.46959,
                             23.83416,
                             0.0
                           ],
                           [
                             90.469574,
                             23.8342018,
                             0.0
                           ],
                           [
                             90.46954,
                             23.8342,
                             0.0
                           ],
                           [
                             90.46945,
                             23.8342075,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-16306",
                       "description": "CS Dag: 16306<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "16306.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.469574,
                             23.8342018,
                             0.0
                           ],
                           [
                             90.46959,
                             23.83416,
                             0.0
                           ],
                           [
                             90.469635,
                             23.83417,
                             0.0
                           ],
                           [
                             90.46962,
                             23.8342056,
                             0.0
                           ],
                           [
                             90.469574,
                             23.8342018,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-16308",
                       "description": "CS Dag: 16308<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "16308.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4695,
                             23.8342037,
                             0.0
                           ],
                           [
                             90.46954,
                             23.8342,
                             0.0
                           ],
                           [
                             90.469574,
                             23.8342018,
                             0.0
                           ],
                           [
                             90.46962,
                             23.8342056,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.8342857,
                             0.0
                           ],
                           [
                             90.4696,
                             23.8342819,
                             0.0
                           ],
                           [
                             90.4695358,
                             23.83427,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.8342686,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.83424,
                             0.0
                           ],
                           [
                             90.4695,
                             23.8342037,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-16307",
                       "description": "CS Dag: 16307<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "16307.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4695053,
                             23.8342686,
                             0.0
                           ],
                           [
                             90.4695358,
                             23.83427,
                             0.0
                           ],
                           [
                             90.4696,
                             23.8342819,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.8342857,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.83431,
                             0.0
                           ],
                           [
                             90.4696,
                             23.83434,
                             0.0
                           ],
                           [
                             90.46958,
                             23.834341,
                             0.0
                           ],
                           [
                             90.4695,
                             23.834341,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.8342686,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14086",
                       "description": "CS Dag: 14086<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14086.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46937,
                             23.8341675,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8341789,
                             0.0
                           ],
                           [
                             90.4693756,
                             23.83422,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8342381,
                             0.0
                           ],
                           [
                             90.46937,
                             23.83427,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8342857,
                             0.0
                           ],
                           [
                             90.46936,
                             23.834322,
                             0.0
                           ],
                           [
                             90.46929,
                             23.8343258,
                             0.0
                           ],
                           [
                             90.46927,
                             23.8343029,
                             0.0
                           ],
                           [
                             90.46927,
                             23.8342533,
                             0.0
                           ],
                           [
                             90.46927,
                             23.8341732,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8341675,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14090",
                       "description": "CS Dag: 14090<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14090.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46962,
                             23.8342056,
                             0.0
                           ],
                           [
                             90.469635,
                             23.83417,
                             0.0
                           ],
                           [
                             90.4697,
                             23.8341866,
                             0.0
                           ],
                           [
                             90.46969,
                             23.834219,
                             0.0
                           ],
                           [
                             90.46967,
                             23.8342152,
                             0.0
                           ],
                           [
                             90.46966,
                             23.8342113,
                             0.0
                           ],
                           [
                             90.46962,
                             23.8342056,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-16309",
                       "description": "CS Dag: 16309<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "16309.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46929,
                             23.8343258,
                             0.0
                           ],
                           [
                             90.46936,
                             23.834322,
                             0.0
                           ],
                           [
                             90.46935,
                             23.8343983,
                             0.0
                           ],
                           [
                             90.4692841,
                             23.8343887,
                             0.0
                           ],
                           [
                             90.46929,
                             23.8343258,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14040",
                       "description": "CS Dag: 14040<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14040.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4696,
                             23.83434,
                             0.0
                           ],
                           [
                             90.46961,
                             23.8344116,
                             0.0
                           ],
                           [
                             90.46956,
                             23.834425,
                             0.0
                           ],
                           [
                             90.4695358,
                             23.8344269,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.83443,
                             0.0
                           ],
                           [
                             90.4694748,
                             23.8344421,
                             0.0
                           ],
                           [
                             90.46941,
                             23.834444,
                             0.0
                           ],
                           [
                             90.46939,
                             23.834444,
                             0.0
                           ],
                           [
                             90.4693756,
                             23.8345051,
                             0.0
                           ],
                           [
                             90.46933,
                             23.8344955,
                             0.0
                           ],
                           [
                             90.4693451,
                             23.834444,
                             0.0
                           ],
                           [
                             90.4693451,
                             23.8344345,
                             0.0
                           ],
                           [
                             90.46935,
                             23.8344,
                             0.0
                           ],
                           [
                             90.46936,
                             23.8343239,
                             0.0
                           ],
                           [
                             90.46937,
                             23.83428,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8342667,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8342342,
                             0.0
                           ],
                           [
                             90.4693756,
                             23.83422,
                             0.0
                           ],
                           [
                             90.46941,
                             23.8342152,
                             0.0
                           ],
                           [
                             90.46945,
                             23.8342075,
                             0.0
                           ],
                           [
                             90.4695,
                             23.8342037,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.83424,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.8342743,
                             0.0
                           ],
                           [
                             90.4695,
                             23.834341,
                             0.0
                           ],
                           [
                             90.4696,
                             23.83434,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14085",
                       "description": "CS Dag: 14085<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14085.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4693756,
                             23.83422,
                             0.0
                           ],
                           [
                             90.46937,
                             23.8342,
                             0.0
                           ],
                           [
                             90.46937,
                             23.83418,
                             0.0
                           ],
                           [
                             90.46937,
                             23.83416,
                             0.0
                           ],
                           [
                             90.46936,
                             23.8341389,
                             0.0
                           ],
                           [
                             90.46936,
                             23.8341179,
                             0.0
                           ],
                           [
                             90.4694443,
                             23.8341274,
                             0.0
                           ],
                           [
                             90.46945,
                             23.8342075,
                             0.0
                           ],
                           [
                             90.46941,
                             23.8342152,
                             0.0
                           ],
                           [
                             90.4693756,
                             23.83422,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-16319",
                       "description": "CS Dag: 16319<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "16319.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                          [
                             90.46947,
                             23.8344398,
                             0
                          ],
                           [
                             90.4694698,
                             23.8344515,
                             0
                           ],
                           [
                             90.4694638,
                             23.8344981,
                             0
                           ],
                           [
                             90.4694598,
                             23.834522,
                             0
                           ],
                           [
                             90.4694584,
                             23.8345281,
                             0
                           ],
                           [
                             90.4694319,
                             23.8345195,
                             0
                           ],
                           [
                             90.4694229,
                             23.8345177,
                             0
                           ],
                           [
                             90.4694159,
                             23.8345143,
                             0
                           ],
                           [
                             90.4693754,
                             23.8345036,
                             0
                           ],
                           [
                             90.4693862,
                             23.8344423,
                             0
                           ],
                           [
                             90.4694083,
                             23.8344429,
                             0
                           ],
                           [
                             90.4694326,
                             23.8344417,
                             0
                           ],
                           [
                             90.46947,
                             23.8344398,
                             0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14084",
                       "description": "CS Dag: 14084<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14084.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4695,
                             23.8344288,
                             0.0
                           ],
                           [
                             90.46948,
                             23.8345356,
                             0.0
                           ],
                           [
                             90.46946,
                             23.834528,
                             0.0
                           ],
                           [
                             90.46947,
                             23.83449,
                             0.0
                           ],
                           [
                             90.46947,
                             23.8344574,
                             0.0
                           ],
                           [
                             90.46947,
                             23.8344517,
                             0.0
                           ],
                           [
                             90.46947,
                             23.83444,
                             0.0
                           ],
                           [
                             90.4695,
                             23.8344288,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14083",
                       "description": "CS Dag: 14083<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14083.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4697,
                             23.8341866,
                             0.0
                           ],
                           [
                             90.46975,
                             23.8341961,
                             0.0
                           ],
                           [
                             90.4697647,
                             23.8342075,
                             0.0
                           ],
                           [
                             90.46977,
                             23.8342113,
                             0.0
                           ],
                           [
                             90.4697952,
                             23.83422,
                             0.0
                           ],
                           [
                             90.46979,
                             23.83423,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8342323,
                             0.0
                           ],
                           [
                             90.46977,
                             23.8342323,
                             0.0
                           ],
                           [
                             90.4697647,
                             23.8342323,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342361,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342419,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342457,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342628,
                             0.0
                           ],
                           [
                             90.46973,
                             23.834259,
                             0.0
                           ],
                           [
                             90.46971,
                             23.83426,
                             0.0
                           ],
                           [
                             90.4697,
                             23.8342419,
                             0.0
                           ],
                           [
                             90.469696,
                             23.8342342,
                             0.0
                           ],
                           [
                             90.46969,
                             23.834219,
                             0.0
                           ],
                           [
                             90.4697,
                             23.8341866,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-16310",
                       "description": "CS Dag: 16310<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "16310.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4697952,
                             23.83422,
                             0.0
                           ],
                           [
                             90.46983,
                             23.83423,
                             0.0
                           ],
                           [
                             90.46983,
                             23.8342972,
                             0.0
                           ],
                           [
                             90.46983,
                             23.8343315,
                             0.0
                           ],
                           [
                             90.46981,
                             23.8343315,
                             0.0
                           ],
                           [
                             90.4697647,
                             23.8343239,
                             0.0
                           ],
                           [
                             90.4697342,
                             23.8343143,
                             0.0
                           ],
                           [
                             90.4697342,
                             23.8342953,
                             0.0
                           ],
                           [
                             90.46974,
                             23.8342819,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342628,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342457,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342419,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342361,
                             0.0
                           ],
                           [
                             90.4697647,
                             23.8342323,
                             0.0
                           ],
                           [
                             90.46977,
                             23.8342323,
                             0.0
                           ],
                           [
                             90.46979,
                             23.83423,
                             0.0
                           ],
                           [
                             90.4697952,
                             23.83422,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14609",
                       "description": "CS Dag: 14609<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14609.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46983,
                             23.83423,
                             0.0
                           ],
                           [
                             90.4698944,
                             23.83424,
                             0.0
                           ],
                           [
                             90.46995,
                             23.8342457,
                             0.0
                           ],
                           [
                             90.46995,
                             23.83429,
                             0.0
                           ],
                           [
                             90.46995,
                             23.8343048,
                             0.0
                           ],
                           [
                             90.46983,
                             23.8343315,
                             0.0
                           ],
                           [
                             90.46983,
                             23.8342934,
                             0.0
                           ],
                           [
                             90.46983,
                             23.8342762,
                             0.0
                           ],
                           [
                             90.46983,
                             23.834259,
                             0.0
                           ],
                           [
                             90.46983,
                             23.83423,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14610",
                       "description": "CS Dag: 14610<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14610.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46978,
                             23.8343277,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343811,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343945,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8344212,
                             0.0
                           ],
                           [
                             90.46979,
                             23.8344536,
                             0.0
                           ],
                           [
                             90.46975,
                             23.8344479,
                             0.0
                           ],
                           [
                             90.46972,
                             23.834444,
                             0.0
                           ],
                           [
                             90.4696655,
                             23.83443,
                             0.0
                           ],
                           [
                             90.46961,
                             23.83441,
                             0.0
                           ],
                           [
                             90.4696,
                             23.834343,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.83431,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.8342857,
                             0.0
                           ],
                           [
                             90.46962,
                             23.8342056,
                             0.0
                           ],
                           [
                             90.46966,
                             23.8342113,
                             0.0
                           ],
                           [
                             90.46969,
                             23.834219,
                             0.0
                           ],
                           [
                             90.469696,
                             23.8342342,
                             0.0
                           ],
                           [
                             90.4697,
                             23.8342419,
                             0.0
                           ],
                           [
                             90.46971,
                             23.83426,
                             0.0
                           ],
                           [
                             90.46973,
                             23.834259,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8342628,
                             0.0
                           ],
                           [
                             90.46974,
                             23.8342819,
                             0.0
                           ],
                           [
                             90.4697342,
                             23.8342953,
                             0.0
                           ],
                           [
                             90.4697342,
                             23.8343143,
                             0.0
                           ],
                           [
                             90.4697647,
                             23.8343239,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343277,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14087",
                       "description": "CS Dag: 14087<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14087.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46949,
                             23.834486,
                             0.0
                           ],
                           [
                             90.4695,
                             23.8344288,
                             0.0
                           ],
                           [
                             90.46956,
                             23.8344231,
                             0.0
                           ],
                           [
                             90.46961,
                             23.83441,
                             0.0
                           ],
                           [
                             90.4696655,
                             23.83443,
                             0.0
                           ],
                           [
                             90.46968,
                             23.8344345,
                             0.0
                           ],
                           [
                             90.4697,
                             23.83444,
                             0.0
                           ],
                           [
                             90.46974,
                             23.834446,
                             0.0
                           ],
                           [
                             90.46971,
                             23.83451,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.834486,
                             0.0
                           ],
                           [
                             90.46949,
                             23.834486,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14082",
                       "description": "CS Dag: 14082<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14082.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46995,
                             23.8343048,
                             0.0
                           ],
                           [
                             90.46996,
                             23.83433,
                             0.0
                           ],
                           [
                             90.46994,
                             23.8344746,
                             0.0
                           ],
                           [
                             90.4698944,
                             23.83448,
                             0.0
                           ],
                           [
                             90.46985,
                             23.83447,
                             0.0
                           ],
                           [
                             90.46981,
                             23.834465,
                             0.0
                           ],
                           [
                             90.46979,
                             23.8344536,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343945,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343811,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343468,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8343277,
                             0.0
                           ],
                           [
                             90.46981,
                             23.8343315,
                             0.0
                           ],
                           [
                             90.46983,
                             23.8343315,
                             0.0
                           ],
                           [
                             90.46995,
                             23.8343048,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14608",
                       "description": "CS Dag: 14608<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14608.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46948,
                             23.8345356,
                             0.0
                           ],
                           [
                             90.4695358,
                             23.8345547,
                             0.0
                           ],
                           [
                             90.46952,
                             23.8345966,
                             0.0
                           ],
                           [
                             90.46951,
                             23.8346233,
                             0.0
                           ],
                           [
                             90.4694,
                             23.83462,
                             0.0
                           ],
                           [
                             90.4692841,
                             23.83462,
                             0.0
                           ],
                           [
                             90.46925,
                             23.8346214,
                             0.0
                           ],
                           [
                             90.4692459,
                             23.83459,
                             0.0
                           ],
                           [
                             90.4692459,
                             23.8345737,
                             0.0
                           ],
                           [
                             90.46924,
                             23.8345642,
                             0.0
                           ],
                           [
                             90.4692459,
                             23.8345528,
                             0.0
                           ],
                           [
                             90.46925,
                             23.8345165,
                             0.0
                           ],
                           [
                             90.46926,
                             23.8345013,
                             0.0
                           ],
                           [
                             90.46928,
                             23.83449,
                             0.0
                           ],
                           [
                             90.46933,
                             23.8344936,
                             0.0
                           ],
                           [
                             90.4693756,
                             23.8345032,
                             0.0
                           ],
                           [
                             90.46941,
                             23.8345146,
                             0.0
                           ],
                           [
                             90.46942,
                             23.8345184,
                             0.0
                           ],
                           [
                             90.46944,
                             23.8345222,
                             0.0
                           ],
                           [
                             90.46948,
                             23.8345356,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14076",
                       "description": "CS Dag: 14076<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14076.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46971,
                             23.83451,
                             0.0
                           ],
                           [
                             90.469696,
                             23.83454,
                             0.0
                           ],
                           [
                             90.4696655,
                             23.8345432,
                             0.0
                           ],
                           [
                             90.46963,
                             23.834549,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.8346386,
                             0.0
                           ],
                           [
                             90.46952,
                             23.8345966,
                             0.0
                           ],
                           [
                             90.4695358,
                             23.8345547,
                             0.0
                           ],
                           [
                             90.46948,
                             23.8345356,
                             0.0
                           ],
                           [
                             90.46949,
                             23.834486,
                             0.0
                           ],
                           [
                             90.4695053,
                             23.834486,
                             0.0
                           ],
                           [
                             90.4695358,
                             23.83449,
                             0.0
                           ],
                           [
                             90.46971,
                             23.83451,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14081",
                       "description": "CS Dag: 14081<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14081.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46978,
                             23.8344517,
                             0.0
                           ],
                           [
                             90.46973,
                             23.8345623,
                             0.0
                           ],
                           [
                             90.46971,
                             23.8345833,
                             0.0
                           ],
                           [
                             90.46962,
                             23.8345642,
                             0.0
                           ],
                           [
                             90.46963,
                             23.834549,
                             0.0
                           ],
                           [
                             90.469696,
                             23.83454,
                             0.0
                           ],
                           [
                             90.46974,
                             23.834446,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8344517,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14080",
                       "description": "CS Dag: 14080<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14080.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4696045,
                             23.8346386,
                             0.0
                           ],
                           [
                             90.46962,
                             23.8345642,
                             0.0
                           ],
                           [
                             90.46971,
                             23.8345833,
                             0.0
                           ],
                           [
                             90.46969,
                             23.8346844,
                             0.0
                           ],
                           [
                             90.4696045,
                             23.8346386,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14078",
                       "description": "CS Dag: 14078<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14078.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.4698944,
                             23.83448,
                             0.0
                           ],
                           [
                             90.46989,
                             23.83452,
                             0.0
                           ],
                           [
                             90.46984,
                             23.8345089,
                             0.0
                           ],
                           [
                             90.46985,
                             23.83447,
                             0.0
                           ],
                           [
                             90.4698944,
                             23.83448,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14607",
                       "description": "CS Dag: 14607<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14607.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46969,
                             23.8346844,
                             0.0
                           ],
                           [
                             90.46971,
                             23.8345833,
                             0.0
                           ],
                           [
                             90.46976,
                             23.8344975,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8344517,
                             0.0
                           ],
                           [
                             90.4698,
                             23.8344631,
                             0.0
                           ],
                           [
                             90.46985,
                             23.83447,
                             0.0
                           ],
                           [
                             90.46984,
                             23.8345089,
                             0.0
                           ],
                           [
                             90.46989,
                             23.83452,
                             0.0
                           ],
                           [
                             90.4698,
                             23.8347359,
                             0.0
                           ],
                           [
                             90.46978,
                             23.8347263,
                             0.0
                           ],
                           [
                             90.46969,
                             23.8346844,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14078",
                       "description": "CS Dag: 14079<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14079.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46994,
                             23.8344746,
                             0.0
                           ],
                           [
                             90.469986,
                             23.8344936,
                             0.0
                           ],
                           [
                             90.46997,
                             23.8345337,
                             0.0
                           ],
                           [
                             90.46995,
                             23.8345737,
                             0.0
                           ],
                           [
                             90.4699249,
                             23.8346233,
                             0.0
                           ],
                           [
                             90.46991,
                             23.8346519,
                             0.0
                           ],
                           [
                             90.4699,
                             23.83469,
                             0.0
                           ],
                           [
                             90.46989,
                             23.83472,
                             0.0
                           ],
                           [
                             90.46988,
                             23.834734,
                             0.0
                           ],
                           [
                             90.46985,
                             23.8347263,
                             0.0
                           ],
                           [
                             90.4698,
                             23.8347359,
                             0.0
                           ],
                           [
                             90.46989,
                             23.83452,
                             0.0
                           ],
                           [
                             90.4698944,
                             23.83448,
                             0.0
                           ],
                           [
                             90.46994,
                             23.8344746,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14606",
                       "description": "CS Dag: 14606<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14606.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46946,
                             23.8346214,
                             0.0
                           ],
                           [
                             90.46951,
                             23.8346233,
                             0.0
                           ],
                           [
                             90.46952,
                             23.8345966,
                             0.0
                           ],
                           [
                             90.4697,
                             23.83469,
                             0.0
                           ],
                           [
                             90.469696,
                             23.8347111,
                             0.0
                           ],
                           [
                             90.46945,
                             23.8347187,
                             0.0
                           ],
                           [
                             90.46946,
                             23.8346214,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14077",
                       "description": "CS Dag: 14077<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14077.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.469986,
                             23.8344936,
                             0.0
                           ],
                           [
                             90.47006,
                             23.8345165,
                             0.0
                           ],
                           [
                             90.47001,
                             23.8346539,
                             0.0
                           ],
                           [
                             90.4699249,
                             23.8346233,
                             0.0
                           ],
                           [
                             90.46997,
                             23.8345337,
                             0.0
                           ],
                           [
                             90.469986,
                             23.8344936,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14605",
                       "description": "CS Dag: 14605<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14605.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.47001,
                             23.8346539,
                             0.0
                           ],
                           [
                             90.469986,
                             23.8347187,
                             0.0
                           ],
                           [
                             90.46997,
                             23.8347359,
                             0.0
                           ],
                           [
                             90.46996,
                             23.8347588,
                             0.0
                           ],
                           [
                             90.46994,
                             23.834753,
                             0.0
                           ],
                           [
                             90.46993,
                             23.834753,
                             0.0
                           ],
                           [
                             90.46988,
                             23.834734,
                             0.0
                           ],
                           [
                             90.46991,
                             23.8346519,
                             0.0
                           ],
                           [
                             90.4699249,
                             23.8346233,
                             0.0
                           ],
                           [
                             90.47001,
                             23.8346539,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14604",
                       "description": "CS Dag: 14604<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14604.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "coordinates": [
                         [
                           [
                             90.46925,
                             23.8346214,
                             0.0
                           ],
                           [
                             90.4693,
                             23.8346214,
                             0.0
                           ],
                           [
                             90.46936,
                             23.83462,
                             0.0
                           ],
                           [
                             90.46946,
                             23.8346214,
                             0.0
                           ],
                           [
                             90.46945,
                             23.8347187,
                             0.0
                           ],
                           [
                             90.46942,
                             23.8347244,
                             0.0
                           ],
                           [
                             90.4693756,
                             23.8347263,
                             0.0
                           ],
                           [
                             90.46933,
                             23.8347244,
                             0.0
                           ],
                           [
                             90.46926,
                             23.8347244,
                             0.0
                           ],
                           [
                             90.46926,
                             23.8347,
                             0.0
                           ],
                           [
                             90.46925,
                             23.8346786,
                             0.0
                           ],
                           [
                             90.46925,
                             23.83466,
                             0.0
                           ],
                           [
                             90.46925,
                             23.8346214,
                             0.0
                           ]
                         ]
                       ],
                       "type": "Polygon"
                   },
                   "properties": {
                       "name": "CS-14075",
                       "description": "CS Dag: 14075<br>Khatian No: <br>Owner Name: <br>Land Area: <br>description: ",
                       "CS Dag": "14075.0",
                       "Khatian No": null,
                       "Owner Name": null,
                       "Land Area": null,
                       "description": null
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
             labels = ['<strong>Purchase Status</strong>'],
        

         categories = ['Purchased', 'Hand Baina', 'On Registration', 'Discussion', 'Other'];

             for (var i = 0; i < purprocess.length; i++) {

                 div.innerHTML +=
                 labels.push(
                     '<i class="circle" style="background-color:' + (purprocess[i].gdesc2 ? purprocess[i].gdesc2 : "#3388FF") + '"></i> ' +
                 (purprocess[i].gdesc ? purprocess[i].gdesc : '+'));

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
             labels = ['<strong>LAND LIST</strong>'];


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

