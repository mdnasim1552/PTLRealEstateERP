﻿<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="entryAngularjs.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.entryAngularjs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
   

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper" ng-app="myApp" ng-controller="myContrl" >
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_B">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-6 pading5px ">
                                        <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName" Text="Qty"></asp:Label>
                                        <asp:TextBox ID="txtqty" ng-model="txtqty" runat="server" CssClass=" inputtextbox"></asp:TextBox>


                                        <asp:Label ID="lblrate" runat="server" Text="Rate" TabIndex="1" CssClass=" smLbl_to"></asp:Label>

                                        <asp:TextBox ID="txtrate" ng-model="txtrate" runat="server" CssClass="inputtextbox" TabIndex="2"></asp:TextBox>



                                    </div>
                                    <div class="col-md-2 pading5px">
                                        <h1>{{2+3}}</h1>
                                    


                                    </div>
                                </div>

                                <div class="form-group">

                                    <div class="col-md-3">
                                   <asp:TextBox ID="TextBox1" ng-model="txttest" runat="server" CssClass="inputtextbox" TabIndex="2"></asp:TextBox>

                                    </div>
                                    
                                   
                                </div>
                        </fieldset>
                    </div>
                </div>
            </div>
           <script src="../AngularJs/AgControlerData.js"></script>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

