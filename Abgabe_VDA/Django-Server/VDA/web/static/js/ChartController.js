var DiagramData = {
    // A labels array that can contain any sort of values
    labels: [],
    // Our series array that contains series objects or in this case series data arrays
    series: [
        []
    ]
};

function DdlDiagramSensorChangeEvent(e) {
    var SensorID = $("#" + e.target.id).val() || 0;
    if (SensorID != null && SensorID > 0) {
        objParameter = {
            SensorID: SensorID
        };

        ajaxCall("http://" + window.location.host, "get_diagram_sensor", "POST", JSON.stringify(objParameter), "JSON", SensorDiagramData);
    } else {
        DiagramData.labels = [];
        DiagramData.series = [];
        new Chartist.Line('.ct-chart', DiagramData);
    }
}

function SensorDiagramData(data) {
    console.log(data);
    DiagramData.labels = [];
    DiagramData.series = [];

    var serie = new Array();
    var StartAndEndDate = new Array();

    for (let key in data) {
        var tempDate = new Date(key);
        serie.push(data[key]);
        DiagramData.labels.push(tempDate.getHours() + ":" + tempDate.getMinutes());

        if (StartAndEndDate.length === 0) {
            StartAndEndDate.push(tempDate.toLocaleDateString());
        } else {
            if (StartAndEndDate.length === 1) {
                StartAndEndDate.push(tempDate.toLocaleDateString());
            } else {
                StartAndEndDate[1] = tempDate.toLocaleDateString();
            }
        }
    }

    if (StartAndEndDate.length > 0) {
        var strDate = StartAndEndDate[0];
        if (StartAndEndDate.length > 1 && StartAndEndDate[0] !== StartAndEndDate[1]) {
            strDate += " bis " + StartAndEndDate[1];
        }
        $("#DiagrammDate").html(strDate);
            
    } else {
        $("#DiagrammDate").html("");
    }

    DiagramData.series.push(serie);

    //var objJson = JSON.parse(data);
    //data ist bereits ein JSON Objekt
   // console.log(data);
    //for (let entry of data) {
    //    alert(entry);
    //}

    new Chartist.Line('.ct-chart', DiagramData);
}

$(document).ready(function () {
    $("#ddlDiagramSensor").change(DdlDiagramSensorChangeEvent);

    var SensorID = $("#ddlDiagramSensor").val() || 0;
    if (SensorID !== null && SensorID > 0) {
        objParameter = {
            SensorID: SensorID
        };

        ajaxCall("http://localhost:49181", "get_diagram_sensor", "POST", JSON.stringify(objParameter), "JSON", SensorDiagramData);
    } else {
        new Chartist.Line('.ct-chart', DiagramData);
    }
});
