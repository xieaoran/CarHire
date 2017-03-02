function validateEmail(input){
    var regEXMail = /^[a-z0-9|\-|_]+@[a-z0-9]+(\.[a-z]+)+$/;
    return input.match(regEXMail) != null;
}

function validatePhone(input) {
    var regEXPhone = /^1\d{10}$/;
    return input.match(regEXPhone) != null;
}

function validateTime(input){
    var regEXTime = /^([01]\d|[2][0-3]|\d)(:|：)([0-5]\d)$/;
    return input.match(regEXTime) != null;
}
function padLeft(str, toLength) {
    if (str.length >= toLength)
        return str;
    else
        return padLeft("0" + str, toLength);
}