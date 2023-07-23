//// Get the current time and update the element
//function updateTime() {
//    var currentTimeElement = document.getElementById("current-time");
//    var currentTime = new Date();
//    var hours = currentTime.getHours();
//    var minutes = currentTime.getMinutes();
//    var seconds = currentTime.getSeconds();

//    // Format the time with leading zeros if needed
//    var formattedTime = hours.toString().padStart(2, '0') + ':' + minutes.toString().padStart(2, '0') + ':' + seconds.toString().padStart(2, '0');

//    // Update the element with the current time
//    currentTimeElement.innerHTML = "Current Time: " + formattedTime;
//}

//// Update the current time every second
//setInterval(updateTime, 1000);
function updateClock() {
    var clockElement = document.getElementById('clock');
    var currentTime = new Date();
    var hours = currentTime.getHours();
    var minutes = currentTime.getMinutes();
    var seconds = currentTime.getSeconds();

    // Pad single digits with leading zeros
    hours = (hours < 10 ? '0' : '') + hours;
    minutes = (minutes < 10 ? '0' : '') + minutes;
    seconds = (seconds < 10 ? '0' : '') + seconds;

    var timeString = '<span class="hours">' + hours + '</span>:<span class="minutes">' + minutes + '</span>:<span class="seconds">' + seconds + '</span>';
    clockElement.innerHTML = 'Current Time: ' + timeString;

    // Update the clock every second
    setTimeout(updateClock, 1000);
}

// Start the clock
updateClock();


