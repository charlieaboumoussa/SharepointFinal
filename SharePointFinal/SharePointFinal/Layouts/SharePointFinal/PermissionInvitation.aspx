<%@ Assembly Name="$SharePoint.Project.AssemblyFullName$" %>
<%@ Import Namespace="Microsoft.SharePoint.ApplicationPages" %>
<%@ Register Tagprefix="SharePoint" Namespace="Microsoft.SharePoint.WebControls" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="Utilities" Namespace="Microsoft.SharePoint.Utilities" Assembly="Microsoft.SharePoint, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Register Tagprefix="asp" Namespace="System.Web.UI" Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Import Namespace="Microsoft.SharePoint" %>
<%@ Assembly Name="Microsoft.Web.CommandUI, Version=15.0.0.0, Culture=neutral, PublicKeyToken=71e9bce111e9429c" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermissionInvitation.aspx.cs" Inherits="SharePointFinal.Layouts.SharePointFinal.PermissionInvitation" %>

<html>
    <head>
        <script type="text/javascript"  src="//ajax.googleapis.com/ajax/libs/jquery/1.9.1/jquery.min.js"></script>
        <link rel="stylesheet" type="text/css" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" />
        <link rel="stylesheet" type="text/css" href="//cdn.datatables.net/1.10.19/css/jquery.dataTables.min.css"/>
        <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="//cdn.datatables.net/1.10.19/js/jquery.dataTables.min.js"></script>
        <title>SharePoint Final</title>
    </head>    
    <body onload="getPermissions()">
        <button id="breakSecurity" onclick="breakSecurity()">Break Security</button>
        <table id="myTable">

        </table>
    </body>    
</html>
<script>
    function getPermissions() {
        var table;
        $.ajax({
            url: "PermissionInvitation.aspx/getPermissions",
            method: "POST",
            contentType: "application/json",
            dataType: "json",
            data: "",
            success: function (data) {
                alert(data.d);
                var employeeTableBody = $('#myTable tbody');
                employeeTableBody.empty();
                var permissions = JSON.parse(data.d);
                $(document).ready(function () {
                    table = $('#myTable').DataTable({
                        data: permissions,
                        columns: [
                                    { title: "Name" ,data:"Name"},
                                    { title: "Type",data:"Type" },
                                    { title: "Permission Level",data:"PermissionLevel" }
                                ] 
                    });
                });

                $('#myTable tbody').on('click', 'tr', function () {
                    if ($(this).hasClass('selected')) {
                        $(this).removeClass('selected');
                    }
                    else {
                        table.$('tr.selected').removeClass('selected');
                        $(this).addClass('selected');
                    }
                });
            },
            error: function (data) {
                console.log("==error==" + data.responseText);
            }
        });
    }
    function breakSecurity() {
        $.ajax({
            url: "PermissionInvitation.aspx/breakSecurity",
            method: "POST",
            contentType: "application/json",
            dataType: "json",
            data: "",
            success: function (data) {
            },
            error: function (data) {
            }
        });
    }
</script>
