//Метод: Получить параметры с адресной строки 
function getValueParametr(name) {
    name = name.replace(/[\[]/, "\\\[").replace(/[\]]/, "\\\]");
    var regexS = "[\\?&]" + name + "=([^&#]*)";
    var regex = new RegExp(regexS);
    var results = regex.exec(window.location.href);
    if (results == null)
        return "";
    else
        return results[1];
}
//Метод: Поворот изображения
function Rotate(img, factor) {
    if (img != null) {
        /**
        * Get the rotation factor if any
        */
        img.rotation = 0;
        current = (img.rotation ? Number(img.rotation) : 0) + factor;
        if (factor >= 0) {
            var rotation = Math.PI * current / 180;
        } else {
            var rotation = Math.PI * (360 + current) / 180;
        }
        var costheta = Math.cos(current);
        var sintheta = Math.sin(current);
        // Interweb Explorer
        if (document.all && !window.opera) {
            img.style.filter = "progid:DXImageTransform.Microsoft.Matrix(M11=" + costheta + ",M12=" + (-sintheta) + ",M21=" + sintheta + ",M22=" + costheta + ",SizingMethod='auto expand')";
        } else {
            alert('Операция поворота изображения доступна для выполнения в броузере Internet Explorer');
        }
        img.rotation = current;
        return true;
    }
    else {
        alert("Запись датчика отсутствует");
        return false;
    }
}
//Метод: Поиск датчика в массиве и применение его параметров 
function GetDataSensor() {
    var Id = getValueParametr("ID");
    var S = getValueParametr("S");
    if (Id != "" & S != "") {
        for (var i = 0; i < arrayData.length; i++) {
            if (arrayData[i].length == undefined) {
                TransformSensor(arrayData[3], arrayData[4], arrayData[2], arrayData[5], arrayData[6], arrayData[7]);
                return;
            }
            else {
                var count = arrayData[i].length;
                for (var x = 0; x < count; x++) {
                    if (Id.toString() == arrayData[i][0].toString()) {
                        if (S.toString() == arrayData[i][1].toString()) {
                            TransformSensor(arrayData[i][3], arrayData[i][4], arrayData[i][2], arrayData[i][5], arrayData[i][6], arrayData[i][7]);
                            return;}}}}}
        var removeImage = document.getElementById('ImageDiv');
        if (removeImage != null) {
            removeImage.innerHTML = "";
        }
        
        var textAlarm = document.getElementById('AlarmText');
        var alarmCell = document.getElementById('AlarmTextCell');
        alarmCell.setAttribute("vAlign", "middle");
        alarmCell.setAttribute("Align", "center");
        textAlarm.innerHTML = "ТЕКСТ<br>СООБЩЕНИЯ";

        alert("Введенные данные не найдены в массиве датчиков");
    }
    else {
        var removeImage = document.getElementById('ImageDiv');
        if (removeImage!=null) {
            removeImage.innerHTML = "";
            
            var textAlarm = document.getElementById('AlarmText');
            var alarmCell = document.getElementById('AlarmTextCell');
            alarmCell.setAttribute("vAlign", "middle");
            alarmCell.setAttribute("Align", "center");
            textAlarm.innerHTML = "ТЕКСТ<br>СООБЩЕНИЯ";
           
            alert("Введите параметры для отображения датчика");
        }
    }
}
//Метод: Поиск датчика в массиве и применение его параметров 
function TransformSensor(x, y, path, rotat, AlarmText, textColor) {
    var element = document.getElementById('sensor');
    var element2 = document.getElementById('ImageDiv');
    if (element != null) {
        if (path!=null) {
            element.setAttribute('src', path.toString());
        }
        element.removeAttribute('width');
        element.removeAttribute('height');
        element.removeAttribute('rotation');

        var left = (document.all.ImageCell.clientWidth - document.all.Plan.clientWidth) / 2;
        var top = (document.all.ImageCell.clientHeight - document.all.Plan.clientHeight) / 2;

        var nValueLeft = parseInt(left) + parseInt(x);
        var nValueTop = parseInt(top) + parseInt(y);

        element2.style.left = nValueLeft;
        element2.style.top = nValueTop;

        Rotate(document.getElementById('sensor'), rotat);

        element.removeAttribute('width');
        element.removeAttribute('height');
        element.removeAttribute('rotation');

        var alarm = document.getElementById('AlarmText');
        alarm.style.color = textColor;

        var alarmCell = document.getElementById('AlarmTextCell');
        alarmCell.setAttribute("vAlign", "top");
        if (AlarmText != "" ) {
            alarm.innerHTML = "!!!ВНИМАНИЕ!!!<br>" + AlarmText;
        } else {
        alarm.innerHTML = " ТЕКСТ СООБЩЕНИЯ ПУСТ";
        }
        
    }
}