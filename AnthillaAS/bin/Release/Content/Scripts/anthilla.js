var localhost = '/gw/';

$(document).ready(function () { $('input:text').attr('autocomplete', 'off'); });

//General

//$(document).ready(function () {
//    $('.table').dataTable();
//});

function Reset() {
    $('.item').hide();
    $('select').prop('selectedIndex', 0);
    $('.project-selectable').removeClass('picked');
    $('.group-selectable').removeClass('picked');
    $('.js-files').hide();
    $("input:text").each(function () {
        $(this).val("");
    });
}

function SetCreate() {
    var button = $('#create-button');
    if (button != null) {
        button.toggleClass('fg-anthilla-green');
        button.toggleClass('no-overlay');
        button.toggleClass('fg-anthilla-gray');
        button.toggleClass('bg-anthilla-green');
    }
    $('#DashboardForm').toggle();
}

function Quit() {
    Reset();
    SetCreate();
}

function SelectizeInput(guid) {
    $('#' + guid + 'TagsEdit').selectize({
        delimiter: ',',
        persist: false,
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    });
}

$('#UGroupTag').selectize({
    delimiter: ',',
    persist: false,
    create: function (input) {
        return {
            value: input,
            text: input
        }
    }
});

$(document).ready(function SelectizeInput() {
    $('.tag-input').selectize({
        delimiter: ',',
        persist: false,
        create: function (input) {
            return {
                value: input,
                text: input
            }
        }
    });
});

$('.selectable').on('click', function (e) {
    $(this).toggleClass("selected");
});

//Single pages
function EditCompany(guid) {
    jQuery.support.cors = true;
    var company = {
        Alias: $('#' + guid + 'AliasEdit').val(),
        Tags: $('#' + guid + 'TagsEdit').val(),
    };
    $.ajax({
        url: '/company/' + guid + '/' + company.Alias + '/' + company.Tags + '/edit',
        type: 'POST',
        data: JSON.stringify(company),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            location.reload(true);
        }
    });
}

function EditProject(guid) {
    jQuery.support.cors = true;
    var project = {
        Alias: $('#' + guid + 'AliasEdit').val(),
        Tags: $('#' + guid + 'TagsEdit').val(),
    };
    $.ajax({
        url: '/project/' + guid + '/' + project.Alias + '/' + project.Tags + '/edit',
        type: 'POST',
        data: JSON.stringify(project),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            location.reload(true);
        }
    });
}

//User
function EditUser(guid) {
    jQuery.support.cors = true;
    var element = {
        FNAME: $('#' + guid + 'fNameEdit').val(),
        LNAME: $('#' + guid + 'lNameEdit').val(),
        UserTag: $('#' + guid + 'TagsEdit').val(),
        UserEmail: $('#' + guid + 'EmailEdit').val(),
        Company: $('#' + guid + 'CompanyEdit option:selected').attr('guid'),
    };
    $.ajax({
        url: '/user/' + guid + '/' + element.FNAME + '/' + element.LNAME + '/reset/' + element.UserEmail + '/' + element.UserTag + '/e',
        type: 'POST',
        data: JSON.stringify(element),
        success: function (data) {
            if (element.Company != null)
            { AssignCompany(guid, element.Company); }
            $('#' + guid + 'ProjectEdit .picked').each(function () {
                var pid = $(this).attr('guid');
                AssignProject(guid, pid);
            });
            $('#' + guid + 'UgroupEdit .picked').each(function () {
                var gid = $(this).attr('guid');
                AssignGroup(guid, gid);
            });
        },
        error: function (x, y, z, jqXHR, textStatus, errorThrown) {
            alert(x + '\n' + y + '\n' + z + textStatus + " - " + errorThrown);
        }
    });
}

function ResetPassword(guid) {
    jQuery.support.cors = true;
    var element = {
        UserPassword: $('#' + guid + 'PasswordReset').val(),
    };
    $.ajax({
        url: localhost + 'reset/password/' + guid + '/' + element.UserPassword,
        type: 'POST',
        data: JSON.stringify(element),
        success: function (data) {
            alert('Password changed successfully!');
        },
        error: function (x, y, z, jqXHR, textStatus, errorThrown) {
            alert(x + '\n' + y + '\n' + z + textStatus + " - " + errorThrown);
        }
    });
}

//Timing Edit
function EditTimingEvent(guid) {
    jQuery.support.cors = true;
    var e = {
        Alias: $('#' + guid + 'AliasEdit').val(),
        Type: $('#' + guid + 'TypeEdit').val(),
        Detail: $('#' + guid + 'DetailEdit').val(),
        Date: $('#' + guid + 'DateEdit').val(),
        Start: $('#' + guid + 'StartEdit').val(),
        End: $('#' + guid + 'EndEdit').val(),
        Tags: $('#' + guid + 'TagsEdit').val(),
    };
    $.ajax({
        url: '/timing/' + guid + '/' + e.Alias + '/' + e.Type + '/' + e.Detail + '/' + e.Date + '/' + e.Start + '/' + e.End + '/' + e.Tags + '/edit',
        type: 'POST',
        data: JSON.stringify(company),
        contentType: "application/json;charset=utf-8",
        success: function (data) {
            location.reload(true);
        }
    });
}

var UgBtn = $('#create-button-ug');
var FgBtn = $('#create-button-fg');
var RgBtn = $('#create-button-rg');

function SwitchBtnCol(btn) {
    btn.toggleClass('fg-anthilla-green');

    btn.toggleClass('fg-anthilla-gray');
    btn.toggleClass('bg-anthilla-green');
}

function ResetBtnCols(btn1, btn2) {
    btn1.removeClass('fg-anthilla-green');

    btn1.removeClass('fg-anthilla-gray');
    btn1.removeClass('bg-anthilla-green');

    btn2.removeClass('fg-anthilla-green');

    btn2.removeClass('fg-anthilla-gray');
    btn2.removeClass('bg-anthilla-green');
}

function SetCreateUG() {
    SwitchBtnCol(UgBtn);

    ResetBtnCols(FgBtn, RgBtn);
    //FgBtn.addClass('fg-anthilla-green');
    //FgBtn.addClass('no-overlay');
    //FgBtn.removeClass('fg-anthilla-gray');
    //FgBtn.removeClass('bg-anthilla-green');

    //RgBtn.addClass('fg-anthilla-green');
    //RgBtn.addClass('no-overlay');
    //RgBtn.removeClass('fg-anthilla-gray');
    //RgBtn.removeClass('bg-anthilla-green');

    $('#UserGroupForm').toggle();
    $('#FunctionGroupForm').hide();
    $('#GroupRelationForm').hide();
}
function SetCreateFG() {
    SwitchBtnCol(FgBtn);

    UgBtn.addClass('fg-anthilla-green');
    UgBtn.addClass('no-overlay');
    UgBtn.removeClass('fg-anthilla-gray');
    UgBtn.removeClass('bg-anthilla-green');

    RgBtn.addClass('fg-anthilla-green');
    RgBtn.addClass('no-overlay');
    RgBtn.removeClass('fg-anthilla-gray');
    RgBtn.removeClass('bg-anthilla-green');

    $('#UserGroupForm').hide();
    $('#FunctionGroupForm').toggle();
    $('#GroupRelationForm').hide();
}
function SetCreateRG() {
    SwitchBtnCol(RgBtn);

    UgBtn.addClass('fg-anthilla-green');
    UgBtn.addClass('no-overlay');
    UgBtn.removeClass('fg-anthilla-gray');
    UgBtn.removeClass('bg-anthilla-green');

    FgBtn.addClass('fg-anthilla-green');
    FgBtn.addClass('no-overlay');
    FgBtn.removeClass('fg-anthilla-gray');
    FgBtn.removeClass('bg-anthilla-green');

    $('#UserGroupForm').hide();
    $('#FunctionGroupForm').hide();
    $('#GroupRelationForm').toggle();
}

function QuitUG() {
    Reset();
    $('.dashboard-header>a').show();
    $('.dashboard-content').hide();
    $('.dashboard-content>#DashboardUG').hide();
}

function QuitFG() {
    Reset();
    $('.dashboard-header>a').show();
    $('.dashboard-content').hide();
    $('.dashboard-content>#DashboardFG').hide();
}

function QuitRG() {
    Reset();
    $('.dashboard-header>a').show();
    $('.dashboard-content').hide();
    $('.dashboard-content>#DashboardRG').hide();
}

//Feedback
function SetFeedback() {
    $('#FeedbackForm').toggle();
}

function QuitFeedback() {
    $('#FeedbackForm').hide();
}

//Selectize
var ItemSelectizer = function () {
    return {
        loadOptions: function (query, callback) {
            if (!query.length) return callback();
            $.ajax({
                url: this.settings.remoteUrl,
                type: 'GET',
                dataType: 'json',
                data: {
                    s: query
                },
                error: function () {
                    callback();
                },
                success: function (data) {
                    callback(data);
                }
            });
        },
        renderOptions: function (data, escape) {
            return '<div><span id="' + escape(data.guid) + '" class="button name bg-anthilla-violet">' + escape(data.alias) + '</span></div>';
        }
    };
}();

    $('#select-user').selectize({
        maxItems: 1,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/user',
        load: ItemSelectizer.loadOptions
    });

    $('#select-user-multi').selectize({
        maxItems: 1,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/user',
        load: ItemSelectizer.loadOptions
    });

    $('#select-company').selectize({
        maxItems: 1,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/company',
        load: ItemSelectizer.loadOptions
    });

    $('#select-company-multi').selectize({
        maxItems: false,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/company',
        load: ItemSelectizer.loadOptions
    });

    $('#select-group').selectize({
        maxItems: false,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/ugroup',
        load: ItemSelectizer.loadOptions
    });

    $('#select-group-mono').selectize({
        maxItems: 1,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/ugroup',
        load: ItemSelectizer.loadOptions
    });

    $('#select-funcgrp-mono').selectize({
        maxItems: false,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/funcgrp',
        load: ItemSelectizer.loadOptions
    });

    $('#select-project').selectize({
        maxItems: false,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/project',
        load: ItemSelectizer.loadOptions
    });

    $('#select-project-mono').selectize({
        maxItems: 1,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ItemSelectizer.renderOptions },
        remoteUrl: '/rawdata/project',
        load: ItemSelectizer.loadOptions
    });

var ContactSelectizer = function () {
    return {
        loadOptions: function (query, callback) {
            if (!query.length) return callback();
            $.ajax({
                url: this.settings.remoteUrl,
                type: 'GET',
                dataType: 'json',
                data: {
                    s: query
                },
                error: function () {
                    callback();
                },
                success: function (data) {
                    callback(data);
                }
            });
        },
        renderOptions: function (data, escape) {
            return '<div>' +
                '<span id="' + escape(data.guid) + '" class="button name bg-anthilla-violet">' +
                escape(data.fname) + ' ' + escape(data.lname) + ' - ' + escape(data.email) +
                '</span>' +
                '</div>';
        }
    };
}();

$(document).ready(function UserContactSelectizer() {
    $('#select-user-contact').selectize({
        maxItems: 1,
        valueField: 'alias',
        labelField: 'alias',
        searchField: 'alias',
        create: false,
        render: { option: ContactSelectizer.renderOptions },
        remoteUrl: '/rawdata/user',
        load: ContactSelectizer.loadOptions
    });
});