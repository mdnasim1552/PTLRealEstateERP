<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectMapView2.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.ProjectMapView2" %>
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
            url: "ProjectMapView2.aspx/GetLandInfo",
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
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.46945044110454,
                             23.834207699999972
                           ],
                           [
                             90.46944494110453,
                             23.834126999999977
                           ],
                           [
                             90.46951398253441,
                             23.834139954843963
                           ],
                           [
                             90.46958704110453,
                             23.834159599999975
                           ],
                           [
                             90.46957704110451,
                             23.83420119999997
                           ],
                           [
                             90.46954744110452,
                             23.83420059999996
                           ],
                           [
                             90.46945044110454,
                             23.834207699999972
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "stroke": "#555555",
                       "stroke-width": 2,
                       "stroke-opacity": 1,
                       "fill": "#9941cc",
                       "fill-opacity": 0.5,
                       "name": "CS-16306",
                       "description": "Thi is Test",
                       "CS Dag": "16306.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4695757,
                             23.8342012,
                             0
                           ],
                           [
                             90.4695857,
                             23.8341596,
                             0
                           ],
                           [
                             90.4696347,
                             23.8341702,
                             0
                           ],
                           [
                             90.4696209,
                             23.8342049,
                             0
                           ],
                           [
                             90.4695757,
                             23.8342012,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-16308",
                       "description": "",
                       "CS Dag": "16308.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4695014,
                             23.834204,
                             0
                           ],
                           [
                             90.4695397,
                             23.8342003,
                             0
                           ],
                           [
                             90.4695718,
                             23.8342009,
                             0
                           ],
                           [
                             90.4696216,
                             23.8342049,
                             0
                           ],
                           [
                             90.4696054,
                             23.8342855,
                             0
                           ],
                           [
                             90.4695973,
                             23.8342819,
                             0
                           ],
                           [
                             90.4695356,
                             23.8342708,
                             0
                           ],
                           [
                             90.4695035,
                             23.834269,
                             0
                           ],
                           [
                             90.4695041,
                             23.8342395,
                             0
                           ],
                           [
                             90.4695014,
                             23.834204,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-16307",
                       "description": "",
                       "CS Dag": "16307.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4695035,
                             23.834269,
                             0
                           ],
                           [
                             90.4695363,
                             23.8342711,
                             0
                           ],
                           [
                             90.469598,
                             23.8342822,
                             0
                           ],
                           [
                             90.4696061,
                             23.8342858,
                             0
                           ],
                           [
                             90.4696057,
                             23.8343104,
                             0
                           ],
                           [
                             90.4695996,
                             23.8343398,
                             0
                           ],
                           [
                             90.4695832,
                             23.8343419,
                             0
                           ],
                           [
                             90.4694984,
                             23.834341,
                             0
                           ],
                           [
                             90.4695035,
                             23.834269,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14086",
                       "description": "",
                       "CS Dag": "14086.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4693691,
                             23.8341682,
                             0
                           ],
                           [
                             90.4693703,
                             23.8341795,
                             0
                           ],
                           [
                             90.4693733,
                             23.8342203,
                             0
                           ],
                           [
                             90.4693711,
                             23.8342375,
                             0
                           ],
                           [
                             90.4693684,
                             23.8342712,
                             0
                           ],
                           [
                             90.4693657,
                             23.8342854,
                             0
                           ],
                           [
                             90.4693577,
                             23.8343228,
                             0
                           ],
                           [
                             90.4692886,
                             23.8343259,
                             0
                           ],
                           [
                             90.4692705,
                             23.8343038,
                             0
                           ],
                           [
                             90.4692692,
                             23.8342535,
                             0
                           ],
                           [
                             90.4692678,
                             23.8341737,
                             0
                           ],
                           [
                             90.4693691,
                             23.8341682,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14090",
                       "description": "",
                       "CS Dag": "14090.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696209,
                             23.8342049,
                             0
                           ],
                           [
                             90.4696347,
                             23.8341702,
                             0
                           ],
                           [
                             90.4697044,
                             23.8341866,
                             0
                           ],
                           [
                             90.4696876,
                             23.8342182,
                             0
                           ],
                           [
                             90.4696745,
                             23.8342148,
                             0
                           ],
                           [
                             90.4696544,
                             23.8342105,
                             0
                           ],
                           [
                             90.4696209,
                             23.8342049,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-16309",
                       "description": "",
                       "CS Dag": "16309.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4692883,
                             23.8343259,
                             0
                           ],
                           [
                             90.4693574,
                             23.8343228,
                             0
                           ],
                           [
                             90.4693511,
                             23.8343983,
                             0
                           ],
                           [
                             90.4692841,
                             23.8343885,
                             0
                           ],
                           [
                             90.4692883,
                             23.8343259,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14040",
                       "description": "",
                       "CS Dag": "14040.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4695996,
                             23.834339799999995
                           ],
                           [
                             90.4696108,
                             23.834411000000006
                           ],
                           [
                             90.4695605,
                             23.834425699999993
                           ],
                           [
                             90.4695384,
                             23.834426899999993
                           ],
                           [
                             90.4695028,
                             23.834430000000005
                           ],
                           [
                             90.469472,
                             23.83444160000001
                           ],
                           [
                             90.4694103,
                             23.83444469999999
                           ],
                           [
                             90.4693882,
                             23.834444100000006
                           ],
                           [
                             90.4693774,
                             23.834505399999998
                           ],
                           [
                             90.4693318,
                             23.834496200000007
                           ],
                           [
                             90.4693426,
                             23.83444469999999
                           ],
                           [
                             90.4693426,
                             23.8344349
                           ],
                           [
                             90.4693514,
                             23.834399800000003
                           ],
                           [
                             90.4693577,
                             23.8343243
                           ],
                           [
                             90.469368,
                             23.834280900000003
                           ],
                           [
                             90.4693694,
                             23.8342662
                           ],
                           [
                             90.4693714,
                             23.8342349
                           ],
                           [
                             90.4693733,
                             23.8342218
                           ],
                           [
                             90.4694156,
                             23.8342147
                           ],
                           [
                             90.4694491,
                             23.834207699999997
                           ],
                           [
                             90.4695014,
                             23.834204
                           ],
                           [
                             90.4695041,
                             23.834239500000002
                           ],
                           [
                             90.4695038,
                             23.8342739
                           ],
                           [
                             90.4694977,
                             23.834340700000002
                           ],
                           [
                             90.4695996,
                             23.834339799999995
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14085",
                       "description": "",
                       "CS Dag": "14085.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.469373,
                             23.8342215,
                             0
                           ],
                           [
                             90.4693713,
                             23.8342,
                             0
                           ],
                           [
                             90.46937,
                             23.8341807,
                             0
                           ],
                           [
                             90.469367,
                             23.8341601,
                             0
                           ],
                           [
                             90.4693623,
                             23.8341393,
                             0
                           ],
                           [
                             90.4693569,
                             23.8341178,
                             0
                           ],
                           [
                             90.4694436,
                             23.8341273,
                             0
                           ],
                           [
                             90.4694491,
                             23.834208,
                             0
                           ],
                           [
                             90.4694117,
                             23.834216,
                             0
                           ],
                           [
                             90.469373,
                             23.8342215,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-16319",
                       "description": "",
                       "CS Dag": "16319.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
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
                       ]
                   },
                   "properties": {
                       "name": "CS-14084",
                       "description": "",
                       "CS Dag": "14084.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4695008,
                             23.8344282,
                             0
                           ],
                           [
                             90.4694849,
                             23.8345362,
                             0
                           ],
                           [
                             90.4694584,
                             23.8345281,
                             0
                           ],
                           [
                             90.4694648,
                             23.834489,
                             0
                           ],
                           [
                             90.4694691,
                             23.8344583,
                             0
                           ],
                           [
                             90.4694698,
                             23.8344515,
                             0
                           ],
                           [
                             90.46947,
                             23.8344398,
                             0
                           ],
                           [
                             90.4695008,
                             23.8344282,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14083",
                       "description": "",
                       "CS Dag": "14083.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4697044,
                             23.8341866,
                             0
                           ],
                           [
                             90.469748,
                             23.8341956,
                             0
                           ],
                           [
                             90.4697621,
                             23.8342078,
                             0
                           ],
                           [
                             90.4697694,
                             23.8342109,
                             0
                           ],
                           [
                             90.4697922,
                             23.8342213,
                             0
                           ],
                           [
                             90.4697909,
                             23.8342311,
                             0
                           ],
                           [
                             90.4697828,
                             23.834233,
                             0
                           ],
                           [
                             90.4697748,
                             23.8342324,
                             0
                           ],
                           [
                             90.4697667,
                             23.834233,
                             0
                           ],
                           [
                             90.4697607,
                             23.8342367,
                             0
                           ],
                           [
                             90.4697574,
                             23.834241,
                             0
                           ],
                           [
                             90.4697574,
                             23.8342465,
                             0
                           ],
                           [
                             90.4697553,
                             23.8342636,
                             0
                           ],
                           [
                             90.4697285,
                             23.8342587,
                             0
                           ],
                           [
                             90.4697144,
                             23.8342606,
                             0
                           ],
                           [
                             90.4697004,
                             23.834241,
                             0
                           ],
                           [
                             90.4696977,
                             23.8342336,
                             0
                           ],
                           [
                             90.4696876,
                             23.8342182,
                             0
                           ],
                           [
                             90.4697044,
                             23.8341866,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-16310",
                       "description": "",
                       "CS Dag": "16310.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4697922,
                             23.8342213,
                             0
                           ],
                           [
                             90.4698345,
                             23.8342311,
                             0
                           ],
                           [
                             90.4698338,
                             23.8342968,
                             0
                           ],
                           [
                             90.4698358,
                             23.8343311,
                             0
                           ],
                           [
                             90.469807,
                             23.8343311,
                             0
                           ],
                           [
                             90.4697627,
                             23.8343238,
                             0
                           ],
                           [
                             90.4697339,
                             23.8343146,
                             0
                           ],
                           [
                             90.4697339,
                             23.8342955,
                             0
                           ],
                           [
                             90.4697393,
                             23.834282,
                             0
                           ],
                           [
                             90.4697553,
                             23.8342636,
                             0
                           ],
                           [
                             90.4697574,
                             23.8342465,
                             0
                           ],
                           [
                             90.4697574,
                             23.834241,
                             0
                           ],
                           [
                             90.4697607,
                             23.8342367,
                             0
                           ],
                           [
                             90.4697667,
                             23.834233,
                             0
                           ],
                           [
                             90.4697748,
                             23.8342324,
                             0
                           ],
                           [
                             90.4697909,
                             23.8342311,
                             0
                           ],
                           [
                             90.4697922,
                             23.8342213,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14609",
                       "description": "",
                       "CS Dag": "14609.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4698345,
                             23.8342311,
                             0
                           ],
                           [
                             90.4698962,
                             23.83424,
                             0
                           ],
                           [
                             90.4699458,
                             23.8342449,
                             0
                           ],
                           [
                             90.4699465,
                             23.8342897,
                             0
                           ],
                           [
                             90.4699491,
                             23.8343057,
                             0
                           ],
                           [
                             90.4698358,
                             23.8343311,
                             0
                           ],
                           [
                             90.4698338,
                             23.8342928,
                             0
                           ],
                           [
                             90.4698344,
                             23.8342753,
                             0
                           ],
                           [
                             90.4698338,
                             23.8342596,
                             0
                           ],
                           [
                             90.4698345,
                             23.8342311,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14610",
                       "description": "",
                       "CS Dag": "14610.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4697781,
                             23.834327,
                             0
                           ],
                           [
                             90.4697808,
                             23.8343816,
                             0
                           ],
                           [
                             90.4697797,
                             23.8343939,
                             0
                           ],
                           [
                             90.4697821,
                             23.8344215,
                             0
                           ],
                           [
                             90.4697848,
                             23.834454,
                             0
                           ],
                           [
                             90.4697486,
                             23.8344473,
                             0
                           ],
                           [
                             90.4697217,
                             23.8344436,
                             0
                           ],
                           [
                             90.4696641,
                             23.8344307,
                             0
                           ],
                           [
                             90.4696088,
                             23.8344092,
                             0
                           ],
                           [
                             90.4695983,
                             23.8343427,
                             0
                           ],
                           [
                             90.469605,
                             23.8343101,
                             0
                           ],
                           [
                             90.4696054,
                             23.8342855,
                             0
                           ],
                           [
                             90.4696209,
                             23.8342049,
                             0
                           ],
                           [
                             90.4696544,
                             23.8342105,
                             0
                           ],
                           [
                             90.4696876,
                             23.8342182,
                             0
                           ],
                           [
                             90.4696977,
                             23.8342336,
                             0
                           ],
                           [
                             90.4697004,
                             23.834241,
                             0
                           ],
                           [
                             90.4697144,
                             23.8342606,
                             0
                           ],
                           [
                             90.4697285,
                             23.8342587,
                             0
                           ],
                           [
                             90.4697553,
                             23.8342636,
                             0
                           ],
                           [
                             90.4697393,
                             23.834282,
                             0
                           ],
                           [
                             90.4697339,
                             23.8342955,
                             0
                           ],
                           [
                             90.4697339,
                             23.8343146,
                             0
                           ],
                           [
                             90.4697627,
                             23.8343238,
                             0
                           ],
                           [
                             90.4697781,
                             23.834327,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14087",
                       "description": "",
                       "CS Dag": "14087.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694921,
                             23.8344862,
                             0
                           ],
                           [
                             90.4695008,
                             23.8344282,
                             0
                           ],
                           [
                             90.4695585,
                             23.8344239,
                             0
                           ],
                           [
                             90.4696088,
                             23.8344092,
                             0
                           ],
                           [
                             90.4696641,
                             23.8344307,
                             0
                           ],
                           [
                             90.4696825,
                             23.8344353,
                             0
                           ],
                           [
                             90.4697046,
                             23.8344402,
                             0
                           ],
                           [
                             90.4697395,
                             23.8344464,
                             0
                           ],
                           [
                             90.4697087,
                             23.8345114,
                             0
                           ],
                           [
                             90.4695048,
                             23.8344868,
                             0
                           ],
                           [
                             90.4694921,
                             23.8344862,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14082",
                       "description": "",
                       "CS Dag": "14082.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4699491,
                             23.8343057,
                             0
                           ],
                           [
                             90.4699647,
                             23.8343303,
                             0
                           ],
                           [
                             90.4699405,
                             23.8344751,
                             0
                           ],
                           [
                             90.4698963,
                             23.83448,
                             0
                           ],
                           [
                             90.4698493,
                             23.8344702,
                             0
                           ],
                           [
                             90.4698091,
                             23.8344646,
                             0
                           ],
                           [
                             90.4697848,
                             23.834454,
                             0
                           ],
                           [
                             90.4697797,
                             23.8343939,
                             0
                           ],
                           [
                             90.4697808,
                             23.8343816,
                             0
                           ],
                           [
                             90.4697796,
                             23.8343469,
                             0
                           ],
                           [
                             90.4697781,
                             23.834327,
                             0
                           ],
                           [
                             90.469807,
                             23.8343311,
                             0
                           ],
                           [
                             90.4698358,
                             23.8343311,
                             0
                           ],
                           [
                             90.4699491,
                             23.8343057,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14608",
                       "description": "",
                       "CS Dag": "14608.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694849,
                             23.8345362,
                             0
                           ],
                           [
                             90.4695361,
                             23.8345538,
                             0
                           ],
                           [
                             90.4695233,
                             23.8345973,
                             0
                           ],
                           [
                             90.4695153,
                             23.8346231,
                             0
                           ],
                           [
                             90.4693973,
                             23.83462,
                             0
                           ],
                           [
                             90.4692873,
                             23.8346194,
                             0
                           ],
                           [
                             90.4692524,
                             23.8346206,
                             0
                           ],
                           [
                             90.4692484,
                             23.8345918,
                             0
                           ],
                           [
                             90.4692424,
                             23.834574,
                             0
                           ],
                           [
                             90.4692417,
                             23.8345642,
                             0
                           ],
                           [
                             90.4692464,
                             23.8345525,
                             0
                           ],
                           [
                             90.4692544,
                             23.8345157,
                             0
                           ],
                           [
                             90.4692585,
                             23.834501,
                             0
                           ],
                           [
                             90.4692772,
                             23.8344906,
                             0
                           ],
                           [
                             90.4693298,
                             23.8344944,
                             0
                           ],
                           [
                             90.4693754,
                             23.8345036,
                             0
                           ],
                           [
                             90.4694159,
                             23.8345143,
                             0
                           ],
                           [
                             90.4694229,
                             23.8345177,
                             0
                           ],
                           [
                             90.4694382,
                             23.8345213,
                             0
                           ],
                           [
                             90.4694849,
                             23.8345362,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "stroke": "#555555",
                       "stroke-width": 2,
                       "stroke-opacity": 1,
                       "fill": "#e70d0d",
                       "fill-opacity": 0.5,
                       "name": "CS-14076",
                       "description": "",
                       "CS Dag": "14076.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4697087,
                             23.8345114,
                             0
                           ],
                           [
                             90.4696946,
                             23.8345396,
                             0
                           ],
                           [
                             90.4696678,
                             23.8345426,
                             0
                           ],
                           [
                             90.4696269,
                             23.8345481,
                             0
                           ],
                           [
                             90.4696034,
                             23.8346395,
                             0
                           ],
                           [
                             90.4695233,
                             23.8345973,
                             0
                           ],
                           [
                             90.4695361,
                             23.8345538,
                             0
                           ],
                           [
                             90.4694849,
                             23.8345362,
                             0
                           ],
                           [
                             90.4694921,
                             23.8344862,
                             0
                           ],
                           [
                             90.4695048,
                             23.8344868,
                             0
                           ],
                           [
                             90.4695357,
                             23.8344899,
                             0
                           ],
                           [
                             90.4697087,
                             23.8345114,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14081",
                       "description": "",
                       "CS Dag": "14081.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4697773,
                             23.8344525,
                             0
                           ],
                           [
                             90.4697257,
                             23.8345632,
                             0
                           ],
                           [
                             90.469715,
                             23.834584,
                             0
                           ],
                           [
                             90.4696224,
                             23.8345644,
                             0
                           ],
                           [
                             90.4696269,
                             23.8345481,
                             0
                           ],
                           [
                             90.4696946,
                             23.8345396,
                             0
                           ],
                           [
                             90.4697395,
                             23.8344464,
                             0
                           ],
                           [
                             90.4697773,
                             23.8344525,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14080",
                       "description": "",
                       "CS Dag": "14080.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696034,
                             23.8346395,
                             0
                           ],
                           [
                             90.4696224,
                             23.8345644,
                             0
                           ],
                           [
                             90.469715,
                             23.834584,
                             0
                           ],
                           [
                             90.4696908,
                             23.8346843,
                             0
                           ],
                           [
                             90.4696034,
                             23.8346395,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14078",
                       "description": "",
                       "CS Dag": "14078.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4698963,
                             23.83448,
                             0
                           ],
                           [
                             90.469889,
                             23.8345195,
                             0
                           ],
                           [
                             90.4698427,
                             23.8345097,
                             0
                           ],
                           [
                             90.4698493,
                             23.8344702,
                             0
                           ],
                           [
                             90.4698963,
                             23.83448,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14607",
                       "description": "",
                       "CS Dag": "14607.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4696908,
                             23.8346843,
                             0
                           ],
                           [
                             90.469715,
                             23.834584,
                             0
                           ],
                           [
                             90.4697569,
                             23.8344968,
                             0
                           ],
                           [
                             90.4697773,
                             23.8344525,
                             0
                           ],
                           [
                             90.4698031,
                             23.8344625,
                             0
                           ],
                           [
                             90.4698493,
                             23.8344702,
                             0
                           ],
                           [
                             90.4698427,
                             23.8345097,
                             0
                           ],
                           [
                             90.469889,
                             23.8345195,
                             0
                           ],
                           [
                             90.4698025,
                             23.8347354,
                             0
                           ],
                           [
                             90.4697823,
                             23.8347262,
                             0
                           ],
                           [
                             90.4696908,
                             23.8346843,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14078",
                       "description": "",
                       "CS Dag": "14079.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4699405,
                             23.8344751,
                             0
                           ],
                           [
                             90.4699822,
                             23.8344936,
                             0
                           ],
                           [
                             90.4699735,
                             23.8345329,
                             0
                           ],
                           [
                             90.4699507,
                             23.834574,
                             0
                           ],
                           [
                             90.4699286,
                             23.8346224,
                             0
                           ],
                           [
                             90.4699125,
                             23.8346519,
                             0
                           ],
                           [
                             90.4698984,
                             23.8346899,
                             0
                           ],
                           [
                             90.4698857,
                             23.8347199,
                             0
                           ],
                           [
                             90.469879,
                             23.8347331,
                             0
                           ],
                           [
                             90.4698498,
                             23.8347267,
                             0
                           ],
                           [
                             90.4698025,
                             23.8347354,
                             0
                           ],
                           [
                             90.469889,
                             23.8345195,
                             0
                           ],
                           [
                             90.4698963,
                             23.83448,
                             0
                           ],
                           [
                             90.4699405,
                             23.8344751,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14606",
                       "description": "",
                       "CS Dag": "14606.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4694586,
                             23.8346222,
                             0
                           ],
                           [
                             90.4695153,
                             23.8346231,
                             0
                           ],
                           [
                             90.4695233,
                             23.8345973,
                             0
                           ],
                           [
                             90.4697004,
                             23.8346897,
                             0
                           ],
                           [
                             90.469695,
                             23.8347118,
                             0
                           ],
                           [
                             90.4694549,
                             23.8347179,
                             0
                           ],
                           [
                             90.4694586,
                             23.8346222,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14077",
                       "description": "",
                       "CS Dag": "14077.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4699822,
                             23.8344936,
                             0
                           ],
                           [
                             90.4700624,
                             23.8345164,
                             0
                           ],
                           [
                             90.4700114,
                             23.8346537,
                             0
                           ],
                           [
                             90.4699286,
                             23.8346224,
                             0
                           ],
                           [
                             90.4699735,
                             23.8345329,
                             0
                           ],
                           [
                             90.4699822,
                             23.8344936,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14605",
                       "description": "",
                       "CS Dag": "14605.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4700114,
                             23.8346537,
                             0
                           ],
                           [
                             90.4699825,
                             23.8347196,
                             0
                           ],
                           [
                             90.4699724,
                             23.834735,
                             0
                           ],
                           [
                             90.4699617,
                             23.8347595,
                             0
                           ],
                           [
                             90.4699403,
                             23.8347527,
                             0
                           ],
                           [
                             90.4699322,
                             23.8347527,
                             0
                           ],
                           [
                             90.469879,
                             23.8347331,
                             0
                           ],
                           [
                             90.4699125,
                             23.8346519,
                             0
                           ],
                           [
                             90.4699286,
                             23.8346224,
                             0
                           ],
                           [
                             90.4700114,
                             23.8346537,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14604",
                       "description": "",
                       "CS Dag": "14604.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               },
               {
                   "type": "Feature",
                   "geometry": {
                       "type": "Polygon",
                       "coordinates": [
                         [
                           [
                             90.4692524,
                             23.8346206,
                             0
                           ],
                           [
                             90.4692991,
                             23.8346207,
                             0
                           ],
                           [
                             90.4693622,
                             23.8346195,
                             0
                           ],
                           [
                             90.4694586,
                             23.8346222,
                             0
                           ],
                           [
                             90.4694549,
                             23.8347179,
                             0
                           ],
                           [
                             90.4694198,
                             23.834725,
                             0
                           ],
                           [
                             90.4693729,
                             23.8347262,
                             0
                           ],
                           [
                             90.46933,
                             23.834725,
                             0
                           ],
                           [
                             90.4692575,
                             23.834725,
                             0
                           ],
                           [
                             90.4692602,
                             23.8347004,
                             0
                           ],
                           [
                             90.4692549,
                             23.8346796,
                             0
                           ],
                           [
                             90.4692549,
                             23.8346587,
                             0
                           ],
                           [
                             90.4692524,
                             23.8346206,
                             0
                           ]
                         ]
                       ]
                   },
                   "properties": {
                       "name": "CS-14075",
                       "description": "",
                       "CS Dag": "14075.0",
                       "Khatian No": "",
                       "Owner Name": "",
                       "Land Area": ""
                   }
               }
             ]
         };





        

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

