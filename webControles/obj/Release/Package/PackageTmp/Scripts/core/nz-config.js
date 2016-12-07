var NzConfig = function () {
    var domain = "";
    var domainApi = "http://localhost:64632/api/";

    var getDomain = function () {
        return domain;
    }

    var getDomainApi = function () {
        return domainApi;
    }

    return {
        getDomain: getDomain,
        getDomainApi: getDomainApi
    }
}

var nzConfig = new NzConfig();