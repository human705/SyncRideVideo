var startCnt = -1;
var endCnt = -1;

/**
 * Adds context menus for the map and the created objects.
 * Context menu items can be different depending on the target.
 * That is why in this context menu on the map shows default items as well as
 * the "Add circle", whereas context menu on the circle itself shows the "Remove circle".
 *
 * @param {H.Map} map Reference to initialized map object
 * @param {} mapgroup Reference to initialized group of markers object
 */
function addContextMenus(map, verticegroup) {
  // First we need to subscribe to the "contextmenu" event on the map
  verticegroup.addEventListener('contextmenu', function (e) {
    // As we already handle contextmenu event callback on circle object,
    // we don't do anything if target is different than the map.
    var target = e.target;
    if (!target instanceof H.map.Marker) {
      return;
    }
    // "contextmenu" event might be triggered not only by a pointer,
    // but a keyboard button as well. That's why ContextMenuEvent
    // doesn't have a "currentPointer" property.
    // Instead it has "viewportX" and "viewportY" properties
    // for the associates position.

    // Get geo coordinates from the screen coordinates.
    var coord = target.getGeometry();
    var strMarkerPos = target.getData();
    // console.log("Marker " + target.getData() + " changed coordinates to: " + newLoc.lat + "," + newLoc.lng);
    // var coord  = map.screenToGeo(e.viewportX, e.viewportY);

    // In order to add menu items, you have to push them to the "items"
    // property of the event object. That has to be done synchronously, otherwise
    // the ui component will not contain them. However you can change the menu entry
    // text asynchronously.
    e.items.push(
      // Create a menu item, that has only a label,
      // which displays the current coordinates.
      new H.util.ContextItem({
        label: [
          strMarkerPos.verticeIndex,coord.lat.toFixed(6),coord.lng.toFixed(6)
          // Math.abs(coord.lat.toFixed(4)) + ((coord.lat > 0) ? 'N' : 'S'),
          // Math.abs(coord.lng.toFixed(4)) + ((coord.lng > 0) ? 'E' : 'W')
        ].join(',')
      }),
      // Create an item, that will change the map center when clicking on it.
      new H.util.ContextItem({
        label: 'Center map here',
        callback: function() {
          map.setCenter(coord, true);
        }
      }),
      // It is possible to add a seperator between items in order to logically group them.
      H.util.ContextItem.SEPARATOR,
      // This menu item will add a new circle to the map
      new H.util.ContextItem({
        label: 'Copy to clipboard',
        callback: copyToClipBoard.bind(map, map, coord, strMarkerPos.verticeIndex)
        //callback: addMarker.bind(map, behavior, coord)
      }),
      // This menu item will add a new circle to the map
      new H.util.ContextItem({
        label: 'Add dragable marker',
        //callback: addCircle.bind(map, coord)
        callback: addDraggableMarker.bind(map, map, coord)
      })
    );
  });
}
/**
 * Adds a circle which has a context menu item to remove itself.
 *
 * @this H.Map
 * @param {H.geo.Point} coord Circle center coordinates
 */
function copyToClipBoard(map, coord, pos) {
  // Copy location to clipboard
  var copyText = '';
  var strMyText = pos + ',' + coord.lat + ',' + coord.lng;
  document.getElementById("coords").value = strMyText; 

  var copyText = document.getElementById("coords");
  copyText.select();
  /* Copy the text inside the text field */
  document.execCommand('copy');
  /* Alert the copied text */
  //alert('Copied the text: ' + copyText.value);
  //addPolylineToMap(map,pos);
}

// Find Marker index based on the alt value.
function findMarkerIndex(altValue) {

  var tempPos = -1;
  var found = 0;
  var cntr = 0;
  while (found == 0 && cntr <= markers.length-1) {
    if (markers[cntr].getData() != altValue ) {
      cntr++;
    } else {
      found = 1;
      tempPos = cntr;
    }

  }
  return tempPos;
}

/**
 * Adds a  draggable marker to the map..
 *
 * @param {H.Map} map                      A HERE Map instance within the
 *                                         application
 * @param {H.mapevents.Behavior} behavior  Behavior implements
 *                                         default interactions for pan/zoom
 */
/***********************************************************************************
*
* This function adds a dragable marker on the map. The latlng of the marker is displayed
* in the console for now. Need to:
* 
* 1. add markers to an array to allow for deleting individual ones and print the list
* 2. Add markers to a group so they can be deleted all at once
*
 ************************************************************************************/
function addDraggableMarker(map, coord){

  var marker = new H.map.Marker(coord, {
    // mark the object as volatile for the smooth dragging
    volatility: true
  });
  // Ensure that the marker can receive drag events
  marker.draggable = true;
  map.addObject(marker);
  // Add marker ID using setData
  marker.setData(arrcntr);
  // Add marker to the global array
  markers.push (marker);
  arrcntr++;

  // Subscribe to the "contextmenu" eventas we did for the map.
  marker.addEventListener('contextmenu', function(e) {
    // Add another menu item,
    // that will be visible only when clicking on this object.
    //
    // New item doesn't replace items, which are added by the map.
    // So we may want to add a separator to between them.
    e.items.push(
      new H.util.ContextItem({
        label: 'Remove',
        callback: function() {
          //Remove from the array
          var index = findMarkerIndex(marker.getData())
          var removedMarker = markers.splice(index,1);
          map.removeObject(marker);
        }
      })
    );
  });

  // disable the default draggability of the underlying map
  // and calculate the offset between mouse and target's position
  // when starting to drag a marker object:
  map.addEventListener('dragstart', function(ev) {
    var target = ev.target,
        pointer = ev.currentPointer;
    if (target instanceof H.map.Marker) {
      var targetPosition = map.geoToScreen(target.getGeometry());
      target['offset'] = new H.math.Point(pointer.viewportX - targetPosition.x, pointer.viewportY - targetPosition.y);
      behavior.disable();
    }
  }, false);


  // re-enable the default draggability of the underlying map
  // when dragging has completed
  map.addEventListener('dragend', function(ev) {
    var target = ev.target;
    if (target instanceof H.map.Marker) {
      behavior.enable();
      var newLoc = target.getGeometry();
      console.log("Marker " + target.getData() + " changed coordinates to: " + newLoc.lat + "," + newLoc.lng);
    }
  }, false);

  // Listen to the drag event and move the position of the marker
  // as necessary
  map.addEventListener('drag', function(ev) {
    var target = ev.target,
        pointer = ev.currentPointer;
    if (target instanceof H.map.Marker) {
      target.setGeometry(map.screenToGeo(pointer.viewportX - target['offset'].x, pointer.viewportY - target['offset'].y));
    }
  }, false);

  // Add a bubble to the marker to show 

}


// Write array markers to file
function WriteFile(arrayData) {

  var csv = "";

  for (var i = 0; i < arrayData.length; i++ ) {
    csv += arrayData[i].getData() + "," + arrayData[i].getGeometry().lat + "," + arrayData[i].getGeometry().lng +"\n"
  }

  var csvData = new Blob([csv], { type: 'text/csv' });  
  var csvUrl = URL.createObjectURL(csvData);

  var hiddenElement = document.createElement('a');
  hiddenElement.href = csvUrl;
  hiddenElement.target = '_blank';
  hiddenElement.download = 'test1.csv';
  hiddenElement.click();

  }

  function addPolylineToMap(map, currTimeInSecs) {
    var lineString = new H.geo.LineString();
    var strRawData = document.getElementById('output').textContent
    var strLatLons = strRawData.replace(/\r\n/g, '');
    var arrLatLons = strLatLons.split(',');
    var segmentLenth = 600; // 10 minutes 
    var rideTimeInSecs = arrLatLons.length/2;
    if (rideTimeInSecs < segmentLenth) segmentLenth = rideTimeInSecs;
    var startEndPoints = getMidPoint(currTimeInSecs, rideTimeInSecs, segmentLenth);  //Center of segment in seconds
     // Seconds to array points 
    startCnt = startEndPoints.startPoint * 2;
    endCnt = startEndPoints.endPoint * 2;

    
    var centerMap = false;
    var myLat, myLon
    var i = startCnt;
    while (i < endCnt) 
    {
      myLat = arrLatLons[i];
      i++;
      myLon = arrLatLons[i];
      i++;
      lineString.pushPoint({lat:myLat, lng:myLon});
      if (!centerMap) {
        map.setCenter({lat:myLat, lng:myLon});
        centerMap = true;
      }
    }

     var svgCircle = '<svg width="20" height="20" version="1.1" xmlns="http://www.w3.org/2000/svg">' +
    '<circle cx="10" cy="10" r="7" fill="transparent" stroke="red" stroke-width="4"/>' +
    '</svg>',
    polyline = new H.map.Polyline(
      lineString,
      {
        style: {fillColor: 'rgba(150, 100, 0, .8)', lineWidth: 10}
      }
    ),
    verticeGroup = new H.map.Group({
      visibility: false
    }),
    mainGroup = new H.map.Group({
      volatility: true, // mark the group as volatile for smooth dragging of all it's objects
      objects: [polyline, verticeGroup]
    }),
    polylineTimeout;

    // ensure that the polyline can receive drag events
    polyline.draggable = false;

    // create markers for each polyline's vertice which will be used for dragging
    polyline.getGeometry().eachLatLngAlt(function(lat, lng, alt, index) {
      var vertice = new H.map.Marker(
        {lat, lng},
        {
          icon: new H.map.Icon(svgCircle, {anchor: {x: 10, y: 10}})
        }
      );
      vertice.draggable = false;
      var pointTimeInSecs = (startCnt/2) + index;
      //vertice.setData({'verticeIndex': index})
      vertice.setData({'verticeIndex': pointTimeInSecs})
      verticeGroup.addObject(vertice);

      console.log('Veritce obj data(index) = ' + index + ' pointTimeInSecs = ' + pointTimeInSecs);

    });

    // add group with polyline and it's vertices (markers) on the map
    map.addObject(mainGroup);

    // event listener for main group to show markers if moved in with mouse (or touched on touch devices)
    mainGroup.addEventListener('pointerenter', function(evt) {
      if (polylineTimeout) {
        clearTimeout(polylineTimeout);
        polylineTimeout = null;
      }
    
      // show vertice markers
      verticeGroup.setVisibility(true);
    }, true);

    // event listener for main group to hide vertice markers if moved out with mouse (or released finger on touch devices)
    // the vertice markers are hidden on touch devices after specific timeout
    mainGroup.addEventListener('pointerleave', function(evt) {
      var timeout = (evt.currentPointer.type == 'touch') ? 1000 : 0;
    
      // hide vertice markers
      polylineTimeout = setTimeout(function() {
        verticeGroup.setVisibility(false);
      }, timeout);
    }, true);

    // event listener for vertice markers group to change the cursor to pointer if mouse position enters this group
    verticeGroup.addEventListener('pointerenter', function(evt) {
      document.body.style.cursor = 'pointer';
    }, true);

    // event listener for vertice markers group to change the cursor to default if mouse leaves this group
    verticeGroup.addEventListener('pointerleave', function(evt) {
      document.body.style.cursor = 'default';
    }, true);

    // event listener to copy the marker data to clipboard
    verticeGroup.addEventListener('dbltap', function(evt) {
      var target = evt.target;
      if (target instanceof H.map.Marker) {
        // behavior.enable();
        var newLoc = target.getGeometry();

        //map.setCenter(newLoc);
        console.log("Marker " + target.getData().verticeIndex + " changed coordinates to: " + newLoc.lat + "," + newLoc.lng);

        var copyText = '';
        var strMyText = target.getData().verticeIndex + ',' + newLoc.lat + ',' + newLoc.lng;
        document.getElementById("coords").value = strMyText; 
      
        var copyText = document.getElementById("coords");
        copyText.select();
        /* Copy the text inside the text field */
        document.execCommand('copy');
        /* Alert the copied text */
        //alert('Copied the text: ' + copyText.value);

      }
    }, true);
    // event listener for vertice markers group to resize the geo polyline object if dragging over markers
    verticeGroup.addEventListener('drag', function(evt) {
      var pointer = evt.currentPointer,
          geoLineString = polyline.getGeometry(),
          geoPoint = map.screenToGeo(pointer.viewportX, pointer.viewportY);
    
      // set new position for vertice marker
      evt.target.setGeometry(geoPoint);
    
      // set new position for polyline's vertice
      geoLineString.removePoint(evt.target.getData()['verticeIndex']);
      geoLineString.insertPoint(evt.target.getData()['verticeIndex'], geoPoint);
      polyline.setGeometry(geoLineString);
    
      // stop propagating the drag event, so the map doesn't move
      evt.stopPropagation();
    }, true);

    addContextMenus(map, verticeGroup);
  }

  function initMap() {
  /***************************************************************************************************************************
   * Boilerplate map initialization code starts below:
   */

  /*create array of markers*/
  var markers = new Array();
  var arrcntr = 0;
  
  //Step 1: initialize communication with the platform
  // In your own code, replace variable window.apikey with your own apikey
  var platform = new H.service.Platform({
    apikey: window.apikey
  });
  var defaultLayers = platform.createDefaultLayers();
  
  // var markerLoc 
  
  //Step 2: initialize a map - this map is centered over Boston
  var map = new H.Map(document.getElementById('map'),
    defaultLayers.vector.normal.map, {
    center: {lat:39.931815965, lng:-75.684996962},
    zoom: 15,
    pixelRatio: window.devicePixelRatio || 1
  });
  // add a resize listener to make sure that the map occupies the whole container
  window.addEventListener('resize', () => map.getViewPort().resize());
  
  //Step 3: make the map interactive
  // MapEvents enables the event system
  // Behavior implements default interactions for pan/zoom (also on mobile touch environments)
  var behavior = new H.mapevents.Behavior(new H.mapevents.MapEvents(map));
  
  // Step 4: Create the default UI:
  var ui = H.ui.UI.createDefault(map, defaultLayers, 'en-US');
  
  // Add the click event listener.
  //addDraggableMarker(map, behavior);
  //addContextMenus(map);
  // Now use the map as required...
  var mp = Number(document.getElementById('txtMiddlePoint').value);
  addPolylineToMap(map, mp);

  }

// Returns the middle point of 500 points of the route
function getMidPoint(currPoint, limit, segmentLength) {
  // Anything less than 500 start the route at 0 for 500 points
  //currPoint = Number(document.getElementById('txtbTestRange').value);
  var segmentMiddle = segmentLength / 2;
  var startPoint = 0;
  var endPoint = 0;

  if (currPoint <= segmentMiddle)  {
    startPoint = 0;
    endPoint = segmentLength;
  } else {
    if (currPoint >= (limit - segmentMiddle)) {
      startPoint  = limit - segmentLength;
      endPoint = limit;
    } else {
      startPoint = currPoint - segmentMiddle;
      endPoint = currPoint + segmentMiddle;
    }
  }


  document.getElementById('txtbTestRange').value= startPoint + " - " + endPoint;
  return {startPoint,endPoint};
}

