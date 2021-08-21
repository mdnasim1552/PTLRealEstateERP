﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="GroupChat.aspx.cs" Inherits="RealERPWEB.F_33_Doc.GroupChat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/select2.css" rel="stylesheet" />
    <link href="../Content/jquerysctipttop.css" rel="stylesheet" type="text/css" />
    <link href="../Content/styles.imageuploader.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/select2.js"></script>
    <script src="../Scripts/toaster.js"></script>

    <style>
        span {
            height: 100%;
            width: 100%;
            overflow: hidden;
            padding: 0;
            margin: 0;
            box-sizing: border-box;
        }


        .fa-2x {
            font-size: 1.5em;
        }

        .app {
            position: relative;
            overflow: hidden;
            top: 19px;
            height: 480px; /*calc(100% - 38px);*/
            margin: auto;
            padding: 0;
            box-shadow: 0 1px 1px 0 rgba(0, 0, 0, .06), 0 2px 5px 0 rgba(0, 0, 0, .2);
        }

        .app-one {
            background-color: #f7f7f7;
            height: 100%;
            overflow: hidden;
            margin: 0;
            padding: 0;
            box-shadow: 0 1px 1px 0 rgba(0, 0, 0, .06), 0 2px 5px 0 rgba(0, 0, 0, .2);
        }

        .side {
            padding: 0;
            margin: 0;
            height: 100%;
        }

        .side-one {
            padding: 0;
            margin: 0;
            height: 100%;
            width: 100%;
            z-index: 1;
            position: relative;
            display: block;
            top: 0;
        }

        .side-two {
            padding: 0;
            margin: 0;
            height: 100%;
            width: 100%;
            z-index: 2;
            position: relative;
            top: -100%;
            left: -100%;
            -webkit-transition: left 0.3s ease;
            transition: left 0.3s ease;
        }


        .heading {
            padding: 10px 16px 10px 15px;
            margin: 0;
            height: 60px;
            width: 100%;
            background-color: #eee;
            z-index: 1000;
        }

        .heading-avatar {
            padding: 0;
            cursor: pointer;
        }

        .heading-avatar-icon img {
            border-radius: 50%;
            height: 40px;
            width: 40px;
        }

        .heading-name {
            padding: 0 !important;
            cursor: pointer;
        }

        .heading-name-meta {
            font-weight: 700;
            font-size: 100%;
            padding: 5px;
            padding-bottom: 0;
            text-align: left;
            text-overflow: ellipsis;
            white-space: nowrap;
            color: #000;
            display: block;
        }

        .heading-online {
            /*//display: none;*/
            background-color: chartreuse;
            padding: 0 5px;
            border-radius: 9px 10px;
            margin-left: 10px;
            font-size: 8px;
            color: #93918f;
        }

        .heading-compose {
            padding: 0;
        }

            .heading-compose i {
                text-align: center;
                padding: 5px;
                color: #93918f;
                cursor: pointer;
            }

        .heading-dot {
            padding: 0;
            margin-left: 10px;
        }

            .heading-dot i {
                text-align: right;
                padding: 5px;
                color: #93918f;
                cursor: pointer;
            }

        .searchBox {
            padding: 0 !important;
            margin: 0 !important;
            height: 60px;
            width: 100%;
        }

        .searchBox-inner {
            height: 100%;
            width: 100%;
            padding: 10px !important;
            background-color: #fbfbfb;
        }


            /*#searchBox-inner input {
  box-shadow: none;
}*/

            .searchBox-inner input:focus {
                outline: none;
                border: none;
                box-shadow: none;
            }

        .sideBar {
            padding: 0 !important;
            margin: 0 !important;
            background-color: #fff;
            overflow-y: auto;
            border: 1px solid #f7f7f7;
            height: calc(100% - 120px);
        }

        .sideBar-body {
            position: relative;
            padding: 10px !important;
            border-bottom: 1px solid #f7f7f7;
            height: 72px;
            margin: 0 !important;
            cursor: pointer;
        }

            .sideBar-body:hover {
                background-color: #f2f2f2;
            }

        .sideBar-avatar {
            text-align: center;
            padding: 0 !important;
        }

        .avatar-icon img {
            border-radius: 50%;
            height: 49px;
            width: 49px;
        }

        .sideBar-main {
            padding: 0 !important;
        }

            .sideBar-main .row {
                padding: 0 !important;
                margin: 0 !important;
            }

        .sideBar-name {
            padding: 10px !important;
        }

        .name-meta {
            font-size: 100%;
            padding: 1% !important;
            text-align: left;
            text-overflow: ellipsis;
            white-space: nowrap;
            color: #000;
        }

        .sideBar-time {
            padding: 10px !important;
        }

        .time-meta {
            text-align: right;
            font-size: 12px;
            padding: 1% !important;
            color: rgba(0, 0, 0, .4);
            vertical-align: baseline;
        }

        /*New Message*/

        .newMessage {
            padding: 0 !important;
            margin: 0 !important;
            height: 100%;
            position: relative;
            left: -100%;
        }

        .newMessage-heading {
            padding: 10px 16px 10px 15px !important;
            margin: 0 !important;
            height: 100px;
            width: 100%;
            background-color: #00bfa5;
            z-index: 1001;
        }

        .newMessage-main {
            padding: 10px 16px 0 15px !important;
            margin: 0 !important;
            height: 60px;
            margin-top: 30px !important;
            width: 100%;
            z-index: 1001;
            color: #fff;
        }

        .newMessage-title {
            font-size: 18px;
            font-weight: 700;
            padding: 10px 5px !important;
        }

        .newMessage-back {
            text-align: center;
            vertical-align: baseline;
            padding: 12px 5px !important;
            display: block;
            cursor: pointer;
        }

            .newMessage-back i {
                margin: auto !important;
            }

        .composeBox {
            padding: 0 !important;
            margin: 0 !important;
            height: 60px;
            width: 100%;
        }

        .composeBox-inner {
            height: 100%;
            width: 100%;
            padding: 10px !important;
            background-color: #fbfbfb;
        }

            .composeBox-inner input:focus {
                outline: none;
                border: none;
                box-shadow: none;
            }

        .compose-sideBar {
            padding: 0 !important;
            margin: 0 !important;
            background-color: #fff;
            overflow-y: auto;
            border: 1px solid #f7f7f7;
            height: calc(100% - 160px);
        }

        /*Conversation*/

        .conversation {
            padding: 0 !important;
            margin: 0 !important;
            height: 100%;
            /*width: 100%;*/
            border-left: 1px solid rgba(0, 0, 0, .08);
            /*overflow-y: auto;*/
        }

        .message {
            padding: 0 !important;
            margin: 0 !important;
            background: url("w.jpg") no-repeat fixed center;
            background-size: cover;
            overflow-y: auto;
            border: 1px solid #f7f7f7;
            height: calc(100% - 120px);
        }

        .message-previous {
            margin: 0 !important;
            padding: 0 !important;
            height: auto;
            width: 100%;
        }

        .previous {
            font-size: 15px;
            text-align: center;
            padding: 10px !important;
            cursor: pointer;
        }

            .previous a {
                text-decoration: none;
                font-weight: 700;
            }

        .message-body {
            margin: 0 !important;
            padding: 0 !important;
            width: auto;
            height: auto;
        }

            .message-body img {
                height: 100px !important;
                width: 100px !important;
                float: right;
            }

        .message-main-receiver {
            /*padding: 10px 20px;*/
            max-width: 60%;
        }

        .message-main-sender {
            padding: 3px 20px !important;
            margin-left: 40% !important;
            max-width: 60%;
        }

        .message-text {
            margin: 0 !important;
            padding: 5px !important;
            word-wrap: break-word;
            font-weight: 200;
            font-size: 14px;
            padding-bottom: 0 !important;
        }

        .message-time {
            margin: 0 !important;
            margin-left: 50px !important;
            font-size: 12px;
            text-align: right;
            color: #9a9a9a;
        }

        .receiver {
            width: auto !important;
            padding: 4px 10px 7px !important;
            border-radius: 10px 10px 10px 0;
            background: #ffffff;
            font-size: 12px;
            text-shadow: 0 1px 1px rgba(0, 0, 0, .2);
            word-wrap: break-word;
            display: inline-block;
        }

        .sender {
            float: right;
            width: auto !important;
            background: #dcf8c6;
            border-radius: 10px 10px 0 10px;
            padding: 4px 10px 7px !important;
            font-size: 12px;
            text-shadow: 0 1px 1px rgba(0, 0, 0, .2);
            display: inline-block;
            word-wrap: break-word;
        }


        /*Reply*/

        .reply {
            height: 60px;
            width: 100%;
            background-color: #f5f1ee;
            padding: 10px 5px 10px 5px !important;
            margin: 0 !important;
            z-index: 1000;
        }

        .reply-emojis {
            padding: 5px !important;
        }

            .reply-emojis i {
                text-align: center;
                padding: 5px 5px 5px 5px !important;
                color: #93918f;
                cursor: pointer;
            }

        .reply-recording {
            padding: 5px !important;
        }

            .reply-recording i {
                text-align: center;
                padding: 5px !important;
                color: #93918f;
                cursor: pointer;
            }

        .reply-send {
            padding: 5px !important;
        }

            .reply-send i {
                text-align: center;
                padding: 5px !important;
                color: #93918f;
                cursor: pointer;
            }

        .reply-main {
            padding: 2px 5px !important;
        }

            .reply-main textarea {
                width: 100%;
                resize: none;
                overflow: hidden;
                padding: 5px !important;
                outline: none;
                border: none;
                text-indent: 5px;
                box-shadow: none;
                height: 100%;
                font-size: 16px;
            }

                .reply-main textarea:focus {
                    outline: none;
                    border: none;
                    text-indent: 5px;
                    box-shadow: none;
                }

        @media screen and (max-width: 700px) {
            .app {
                top: 0;
                height: 100%;
            }

            .heading {
                height: 70px;
                background-color: #009688;
            }

            .fa-2x {
                font-size: 2.3em !important;
            }

            .heading-avatar {
                padding: 0 !important;
            }

            .heading-avatar-icon img {
                height: 50px;
                width: 50px;
            }

            .heading-compose {
                padding: 5px !important;
            }

                .heading-compose i {
                    color: #fff;
                    cursor: pointer;
                }

            .heading-dot {
                padding: 5px !important;
                margin-left: 10px !important;
            }

                .heading-dot i {
                    color: #fff;
                    cursor: pointer;
                }

            .sideBar {
                height: calc(100% - 130px);
            }

            .sideBar-body {
                height: 80px;
            }

            .sideBar-avatar {
                text-align: left;
                padding: 0 8px !important;
            }

            .avatar-icon img {
                height: 55px;
                width: 55px;
            }

            .sideBar-main {
                padding: 0 !important;
            }

                .sideBar-main .row {
                    padding: 0 !important;
                    margin: 0 !important;
                }

            .sideBar-name {
                padding: 10px 5px !important;
            }

            .name-meta {
                font-size: 16px;
                padding: 5% !important;
            }

            .sideBar-time {
                padding: 10px !important;
            }

            .time-meta {
                text-align: right;
                font-size: 14px;
                padding: 4% !important;
                color: rgba(0, 0, 0, .4);
                vertical-align: baseline;
            }
            /*Conversation*/
            .conversation {
                padding: 0 !important;
                margin: 0 !important;
                height: 100%;
                /*width: 100%;*/
                border-left: 1px solid rgba(0, 0, 0, .08);
                /*overflow-y: auto;*/
            }

            .message {
                height: calc(100% - 140px);
            }

            .reply {
                height: 70px;
            }

            .reply-emojis {
                padding: 5px 0 !important;
            }

                .reply-emojis i {
                    padding: 5px 2px !important;
                    font-size: 1.8em !important;
                }

            .reply-main {
                padding: 2px 8px !important;
            }

                .reply-main textarea {
                    padding: 8px !important;
                    font-size: 18px;
                }

            .reply-recording {
                padding: 5px 0 !important;
            }

                .reply-recording i {
                    padding: 5px 0 !important;
                    font-size: 1.8em !important;
                }

            .reply-send {
                padding: 5px 0 !important;
            }

                .reply-send i {
                    padding: 5px 2px 5px 0 !important;
                    font-size: 1.8em !important;
                }
        }
    </style>

     <script language="javascript" type="text/javascript">
         $(document).ready(function () {
             //For navigating using left and right arrow of the keyboard
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         });
         function pageLoaded() {
             $('.chzn-select').chosen({ search_contains: true });

         };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:TextBox ID="ParentDir" runat="server" CssClass="hidden"></asp:TextBox>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="app">
                        <div class="row app-one">
                            <div class="col-sm-4 side">
                                <div class="side-one">
                                    <div class="row heading">
                                        <div class="col-sm-3 col-xs-3 heading-avatar">
                                            <div class="heading-avatar-icon">
                                                <asp:Image ID="UserImg" ImageUrl="~/Image/LOGO1a.jpg" runat="server" AlternateText="Image Not Found" /></a>
                                             
            
           
                                            </div>

                                        </div>
                                        <div class="col-sm-1 col-xs-1  heading-dot  pull-right">
                                            <i class="fa fa-ellipsis-v fa-2x  pull-right" aria-hidden="true"></i>
                                        </div>
                                        <div class="col-sm-2 col-xs-2 heading-compose  pull-right">
                                            <i class="fa fa-comments fa-2x  pull-right" aria-hidden="true"></i>
                                        </div>
                                    </div>

                                    <div class="row searchBox">
                                        <div class="col-sm-12 searchBox-inner">
                                            <div class="form-group has-feedback">
                                                <input id="searchText" type="text" class="form-control" name="searchText" placeholder="Search">
                                                <span class="glyphicon glyphicon-search form-control-feedback"></span>
                                            </div>

                                        </div>

                                    </div>

                                    <div class="row sideBar" id="GroupChat">
                                    </div>
                                </div>

                            </div>

                            <div class="col-sm-8 conversation">
                                <div class="row heading">
                                    <div class="col-sm-2 col-md-1 col-xs-3 heading-avatar">
                                        <div class="heading-avatar-icon">
                                            <img src="../Images/member.png">
                                        </div>
                                    </div>
                                    <div class="col-sm-8 col-xs-7 heading-name">
                                        <a class="heading-name-meta"></a>

                                        <i class="projectname"></i>


                                    </div>
                                    <div class="col-sm-2 col-xs-2  heading-dot pull-right">
                                        <a href="#success" data-toggle="modal"><i class="glyphicon glyphicon-pencil"></i>Create Group Chat</a>
                                        <a class="btn btn-xs btn-danger pull-right closebutton" title="Close Chat"><span class="glyphicon glyphicon-off"></span></a>
                                    </div>
                                </div>

                                <div class="row message" id="conversation">
                                    <div class="row message-previous">
                                        <div class="col-sm-12 previous">
                                            <a onclick="previous(this)" id="ankitjain28" name="20">Show Previous Message!
            </a>
                                        </div>
                                    </div>

                                    <div id="MsgSection">
                                    </div>
                                    <%-- <div class="row message-body">
                                        <div class="col-sm-12 message-main-receiver">
                                            <div class="receiver">
                                                <div class="message-text">
                                                    Hi, what are you doing?!
             
                                                </div>
                                                <span class="message-time pull-right">Sun
              </span>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row message-body">
                                        <div class="col-sm-12 message-main-sender">
                                            <div class="sender">
                                                <div class="message-text">
                                                    I am doing nothing man!
             
                                                </div>
                                                <span class="message-time pull-right">Sun
              </span>
                                            </div>
                                        </div>
                                    </div>--%>
                                </div>

                                <div class="row reply">
                                    <div class="col-sm-1 col-xs-1 reply-emojis">
                                        <a href="#ImageUpload" data-toggle="modal"><i class="fa fa-image fa-2x"></i></a>

                                    </div>
                                    <div class="col-sm-9 col-xs-9 reply-main">
                                        <input type="text" class="form-control" onkeypress="return searchKeyPress(event);" style="height: 40px;" id="comment"></input>
                                        <input type="hidden" class="form-control" id="getchatno"></input>
                                    </div>
                                    <div class="col-sm-1 col-xs-1 reply-send">
                                        <a id="sendbtn"><i class="fa fa-send fa-2x" aria-hidden="true"></i></a>

                                    </div>
                                    <div class="col-sm-1 col-xs-1 reply-recording">
                                        <i class="fa fa-microphone fa-2x" aria-hidden="true"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <!-- Modal -->
                    <div class="modal fade" id="success" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header modal-header-success">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h6><i class="glyphicon glyphicon-thumbs-up"></i>Create Group Chat</h6>
                                </div>
                                <div class="modal-body">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <label class="col-md-4">Task Assign From </label>
                                            <div class="col-md-8">
                                                <select id="ddlasin" runat="server" class="chzn-select" style="width: 300px; height: 25px;">
                                                </select>

                                                <%--       <asp:DropDownList ID="ddlasin" runat="server"  Style="width: 300px; height: 25px;">
                                                <asp:ListItem Value="15">Charman</asp:ListItem>
                                                <asp:ListItem Value="20">MD</asp:ListItem>
                                                <asp:ListItem Value="30">GM</asp:ListItem>
                                                <asp:ListItem Value="50">AGM</asp:ListItem>
                                                <asp:ListItem Value="100">Manager</asp:ListItem>
                                                <asp:ListItem Value="150">Employee</asp:ListItem>
                                            </asp:DropDownList>--%>
                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4">Select Project</label>
                                            <div class="col-md-8">
                                                <select id="ddlproject" runat="server" class="chzn-select" style="width: 300px; height: 25px;">
                                                </select>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4">Select Task</label>
                                            <div class="col-md-8">
                                                <select id="ddltask" runat="server" class="chzn-select" style="width: 300px; height: 25px;">
                                                </select>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4">Select Users</label>
                                            <div class="col-md-8">

                                                <%-- <asp:ListBox ID="ddlUser" runat="server" SelectionMode="Multiple"></asp:ListBox>--%>

                                                <select multiple id="ddlUser" class="multiuser" runat="server" style="width: 300px">
                                                </select>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4">Name Of Chat</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtchtname" runat="server" CssClass="" Style="width: 300px; height: 25px;"></asp:TextBox>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4">Message</label>
                                            <div class="col-md-8">
                                                <textarea id="message" style="width: 300px; height: 100px;"></textarea>

                                            </div>

                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-4">Probable Complete Date</label>
                                            <div class="col-md-8">
                                                <asp:TextBox ID="txtprobledate" runat="server" CssClass="inputTxt inputDateBox" TabIndex="5" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="txtprobledate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="modal-footer">
                                    <div class="pull-right">
                                        <button class="getValue btn btn-success" data-dismiss="modal">Create</button>
                                        <button type="button" class="btn btn-default " data-dismiss="modal">Close</button>
                                    </div>

                                </div>
                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->

                    <!-------------------image upload model--------------------------->
                    <div class="modal fade" id="ImageUpload" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header modal-header-success">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h6><i class="glyphicon glyphicon-thumbs-up"></i>Upload Images</h6>
                                </div>
                                <div class="modal-body">
                                    <section role="main" class="l-main">

                                        <div class="uploader__box js-uploader__box l-center-box">
                                            <form action="your/nonjs/fallback/" method="POST">
                                                <div class="uploader__contents">
                                                    <label class="button button--secondary" for="fileinput">Select Files</label>
                                                    <input id="fileinput" class="uploader__file-input" type="file" multiple value="Select Files">
                                                </div>
                                                <input class="button button--big-bottom" type="submit" value="Upload Selected Files">
                                            </form>
                                        </div>
                                    </section>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>







                function searchKeyPress(e) {

                    e = e || window.event;

                    if (e.keyCode == 13) {
                        $(".getValue").off('click');
                        $("#sendbtn").click();

                        return false;
                    }
                    return true;
                }

                $(function () {
                    $(".heading-compose").click(function () {
                        $(".side-two").css({
                            "left": "0"
                        });
                    });

                    $(".newMessage-back").click(function () {
                        $(".side-two").css({
                            "left": "-100%"
                        });
                    });
                })


                $(".closebutton").click(function () {
                    var r = confirm("If Group Chat Cosed You can not continue chat with this Chat.it is a permanently closed. Do you procced? ");
                    if (r == false) {
                        // txt = "You pressed OK!";
                        return;
                    }
                    var chatno = $('#getchatno').val();

                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        url: "GroupChat.aspx/CLoseGroupChat",
                        data: '{chatno: "' + chatno + '" }',
                        dataType: "json",
                        success: function (response) {
                            $.toaster('Group Deleted', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'success');
                            GetGroupChat();
                            //if (data.d == "True") {
                            //    alert("success");

                            //} else {
                            //    alert("failed");
                            //}

                        }
                    });
                });
                $("#sendbtn").click(function () {
                    var message = $('#comment').val();
                    var chatno = $('#getchatno').val();
                    $('#comment').val("");
                    $.ajax({
                        type: "post",
                        contentType: "application/json; charset=utf-8",
                        url: "GroupChat.aspx/SaveChatMsg",
                        data: '{chatno: "' + chatno + '",message:"' + message + '" }',
                        dataType: "json",
                        success: function (response) {
                            $.toaster('Message Sent', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'success');
                            GetChatMsg(chatno);
                            if (data.d == "True") {
                                alert("success");

                            } else {
                                alert("failed");
                            }

                        }
                    });
                });

                function MessageLoader() {
                    var chatno = $('#getchatno').val();
                    if (chatno != "") {
                        GetChatMsg(chatno);
                    }
                }

                function getExtension(path) {
                    var basename = path.split(/[\\/]/).pop();
                    var pos = basename.lastIndexOf('.');

                    if (basename === '' || pos < 1) {
                        return '';
                    }
                    return basename.slice(pos + 1);
                }

                $(document).ready(function () {




                    // setInterval(MessageLoader, 10000)
                    GetGroupChat();
                    $(".closebutton").addClass("hidden");
                    // $("#ddlUser").select2();
                    $('#<%=this.ddlUser.ClientID %>').select2();


                    $(".getValue").click(function () {

                        var userid = $(".multiuser").select2("val");
                        // alert(userid);
                        var chatname = $('#<%=this.txtchtname.ClientID %>').val();
                        // alert(chatname); 
                    var actcode = $('#<%=this.ddlproject.ClientID %>').find(":selected").val();
                        var taskcod = $('#<%=this.ddltask.ClientID %>').find(":selected").val();
                        var asinuser = $('#<%=this.ddlasin.ClientID %>').find(":selected").val();
                        var probdate = $('#<%=this.txtprobledate.ClientID %>').val();

                        var message = $('#message').val();
                        // alert(probdate);
                        $.ajax({
                            type: "post",
                            contentType: "application/json; charset=utf-8",
                            url: "GroupChat.aspx/SaveGroupChat",
                            data: '{userid: "' + userid + '", chatname: "' + chatname + '",actcode:"' + actcode + '",message:"' + message + '",probdate:"' + probdate + '",taskcod:"' + taskcod + '",asinuser:"' + asinuser + '"}',
                            dataType: "json",
                            success: function (response) {
                                if (data.d == "True") {
                                    alert("success");

                                } else {
                                    alert("failed");
                                }

                            }
                        });

                    });

                    $('#GroupChat').delegate('a', 'click', function () {
                        $(".closebutton").removeClass("hidden");
                        var name = $(this).find('.name-meta').html();
                        var chatno = $(this).find('.chatno').html();
                        var actdesc = $(this).find('.actdesc').html();
                        var probledat = $(this).find('.probledat').html();
                        if (probledat == "") {
                            probledat = "";
                        }
                        else {
                            probledat = ", Probable Complete Date: " + probledat;
                        }

                        $(".heading-name-meta").html(name + " <span class='heading-online'></span>");
                        $(".projectname").html(actdesc + probledat);
                        $('#getchatno').val(chatno);
                        GetChatMsg(chatno);
                    });


                    ////////////// send message///////////////////

                });



                var url = $('#<%=this.ParentDir.ClientID %>').val();
                ////////////load msg//////////
                function GetChatMsg(chatno) {
                    $("#MsgSection").empty();

                    $.ajax({
                        type: "POST",
                        url: "GroupChat.aspx/GetChatMsg",
                        data: '{chatno: "' + chatno + '" }',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            console.log(JSON.parse(response.d));
                            var data = JSON.parse(response.d);
                            for (var i = 0; i < data.length; i++) {
                                if (data[i].files == 0) {

                                    $('#MsgSection').append(
                                        '<div class="row message-body">' +
                                        '<div class="col-sm-12 message-main-' + data[i].mestatus + '">' +
                                            '<div class="' + data[i].mestatus + '">' +
                                               ' <div class="message-text">' +
                                                    '' + data[i].message + '' +

                                                '</div><span class="message-time pull-right"> <i>' + data[i].postedname + '</i> , ' + data[i].pday + ' at ' + data[i].ptime + '</span>' +
                                           ' </div></div></div>');
                                }
                                else {
                                    var fileextension = getExtension(data[i].message);
                                    switch (fileextension) {
                                        case "jpg":
                                        case "jpeg":
                                        case "PNG":
                                        case "png":
                                        case "JPEG":
                                        case "GIF":
                                        case "gif":
                                            $('#MsgSection').append(
                                   '<div class="row message-body">' +
                                   '<div class="col-sm-12 message-main-' + data[i].mestatus + '">' +
                                       '<div class="' + data[i].mestatus + '">' +
                                          ' <div class="message-text">' +
                                               '<img class="img img-thumbnail img-responsive chatimage"  src="' + url + data[i].message + '">' +

                                           '</div><span class="message-time pull-right"> <i>' + data[i].postedname + '</i> , ' + data[i].pday + ' at ' + data[i].ptime + '</span>' +
                                      ' </div></div></div>');
                                            break;
                                        case "pdf":
                                        case "PDF":
                                        case "doc":
                                        case "docx":
                                        case "xls":
                                        case "xlsx":
                                            $('#MsgSection').append(
                                  '<div class="row message-body">' +
                                  '<div class="col-sm-12 message-main-' + data[i].mestatus + '">' +
                                      '<div class="' + data[i].mestatus + '">' +
                                         ' <div class="message-text">' +
                                              '<a class="" target="_blank"  href="' + url + data[i].message + '">Download.' + fileextension + '</a>' +

                                          '</div><span class="message-time pull-right"> <i>' + data[i].postedname + '</i> , ' + data[i].pday + ' at ' + data[i].ptime + '</span>' +
                                     ' </div></div></div>');
                                            break;
                                    }

                                }

                            }

                        },
                        failure: function (response) {
                            //  alert(response);
                            alert("f");
                        }
                    });

                }
                function GetGroupChat() {
                    $.ajax({
                        type: "POST",
                        url: "GroupChat.aspx/GetGroupChat",
                        data: '',
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (response) {
                            //  console.log(JSON.parse(response.d));
                            var data = JSON.parse(response.d);
                            // alert(data['sales'][0]['collamt']);
                            // funMonthlyGraph(data)
                            $("#GroupChat").empty();
                            for (var i = 0; i < data.length; i++) {

                                $('#GroupChat').append(
                                    '<a class="loadmsg"><div class="row sideBar-body ">' +
                                                     '<div class="col-sm-3 col-xs-3 sideBar-avatar">' +
                                                                    '<div class="avatar-icon">' +
                                                                       '<img src="../Images/member.png">' +
                                                                   ' </div></div>' +
                                                                '<div class="col-sm-9 col-xs-9 sideBar-main">' +
                                                                    '<div class="row">' +
                                                                        '<div class="col-sm-8 col-xs-8 sideBar-name">' +
                                                                        '<span class="hidden chatno">' + data[i].chatno + '</span>' +
                                                                         '<span class="hidden actdesc">' + data[i].actdesc + '</span>' +
                                                                            '<span class="hidden probledat">' + data[i].probledat + '</span>' +
                                                                          '  <span class="name-meta">' + data[i].chtname + '</span> <br> <i class="small">Created ' + data[i].postedname + ' on: ' + data[i].posteddat + '</i> </div>' +
                                                                        '<div class="col-sm-4 col-xs-4 pull-right sideBar-time">' +
                                                                            '<span class="time-meta pull-right">' + data[i].concern + '<br> <i class="small text-danger ">' + data[i].taskname + '</i></span>' +
                                                                        '</div></div></div></div></a>');
                            }
                        },
                        failure: function (response) {
                            //  alert(response);
                            alert("f");
                        }
                    });
                }



            </script>

            <script src="../Scripts/jquery.imageuploader.js"></script>
            <script>
                (function () {
                    var options = {};
                    $('.js-uploader__box').uploader(options);
                }());
            </script>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

