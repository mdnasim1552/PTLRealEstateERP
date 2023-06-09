﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ImageUpload.aspx.cs" Inherits="RealERPWEB.ImageUpload" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
         
    <script type="text/javascript">
        $(document).ready(function () {

            $('#btnUploadFile').on('click', function () {

                var data = new FormData();

                var files = $("#fileUpload").get(0).files;

                // Add the uploaded image content to the form data collection
                if (files.length > 0) {
                    data.append("UploadedImage", files[0]);
                    data.append("empid", "9300000001");
                }

                // Make Ajax request with the contentType = false, and procesDate = false
                var ajaxRequest = $.ajax({
                    type: "POST",
                    url: "ImageUpload/UploadData",
                    contentType: false,
                    processData: false,
                    data: data
                });

                ajaxRequest.done(function (xhr, textStatus) {
                    // Do other operation
                });
            });
        });
    </script>
    <div class="mt-5 card">
            <label for="fileUpload">
                Select File to Upload:
                <input id="fileUpload" type="file" />

                <input id="btnUploadFile" type="button" value="Upload File" />
        </div>
</asp:Content>
