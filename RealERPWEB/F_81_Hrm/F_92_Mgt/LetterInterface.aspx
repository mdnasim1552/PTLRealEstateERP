<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="LetterInterface.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.LetterInterface" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .card-body{
            min-height:500px;
        }
        .InBox {
            color: red !important;
        }

        .OverAll {
            /*animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: 5;*/
            /*font-size: 18px;*/
            color: black;
            font-size: 14px;
            text-align: center !important;
            margin-top: 0px;
        }
        /* Chrome, Safari, Opera */
        @-webkit-keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
        }

        /* Standard syntax */
        @keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
        }

        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                border-bottom: 0;
            }

                ul.sidebarMenu li:last-child {
                    border-bottom: 1px solid #DFF0D8;
                }

                ul.sidebarMenu li a {
                    text-align: left;
                    display: block;
                    line-height: 30px;
                    font-size: 14px;
                    font-family: Calibri;
                }

                ul.sidebarMenu li h4 {
                    line-height: 50px;
                    text-align: center;
                    display: block;
                }

                ul.sidebarMenu li a:hover {
                    background: #D7E6D1;
                    color: black;
                }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                height: 50px;
                width: 100%;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    height: 50px;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #199698;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            height: 36px;
            width: 100%;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0.5px;
            color: #fff;
            text-align: center;
            border: 1px solid #9752A2;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
        }

        /*.tbMenuWrp table tr td:nth-child(7) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                width: 115px;
                padding: 0 3px;
            }*/


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            cursor: pointer;
            width: 100%!important;
            /*background: whitesmoke;*/
            border-radius: 25px;
            color: #000;
            /*font-weight: bold;*/
            padding: 0 0 0 4px;
            line-height: 30px;
            margin: 1px 2px;
            display: block;
            text-align: left;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                background: #12A5A6;
                color: #fff;
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 30px;
            font-size: 14px;
            color: #000;
            text-align: center;
            float: left;
            font-weight:bold;
        }

        .tbMenuWrp table tr td label span.lbldata {
            /*background: #F3B728;*/
            background-color: #337AB7;
            border: 1px solid #F3B728;
            border-radius: 50%;
            /*display: block;*/
            height: 30px;
            font-size: 11px;
            line-height: 18px;
            margin: 0 5px 0 0;
            padding: 4px 1px;
            width: 28px;
            float: left;
            text-align: center;
            color: #fff;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #12A5A6;
            color: #fff;
        }

        .grvContentarea tr td:last-child {
            width: 120px;
        }

        /* ====================== Gride Under Text ========================*/

        ul li {
            list-style: none;
        }

            ul li a {
                font-size: 11px;
                font-weight: normal;
                line-height: 10px;
                font-family: 'Times New Roman', Times, serif;
                color: #000;
                /*margin-left: 20px;*/
            }

        .chaktxt {
            font-size: 13px;
            font-family: 'Times New Roman', Times, serif;
            color: #000;
            font-weight: normal;
        }

        label {
            display: inline-block;
            font-weight: 400;
            margin-bottom: 5px;
            margin-left: 5px;
            max-width: 100%;
        }

        .displaynone{
            display:none;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="loading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>

                 <div class="card mt-5">
                <div class="card-header">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend ">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="btn btn-secondary btn-sm">Date</asp:Label>
                                </div>
                                <asp:TextBox ID="txtdate" runat="server" CssClass=" form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </div>

                     <div class="card-body">
                         <div class="row">
                         <div class="col-3">
                           <div class="tabMenu">
                        <div class="form-horizontal">
                            <div class="form-group">

                                <div class="tbMenuWrp nav nav-tabs">
                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged">
                                        <asp:ListItem Value="10003" ></asp:ListItem>
                                        <asp:ListItem Value="acceptoffletter"></asp:ListItem>
                                        <asp:ListItem Value="reject"></asp:ListItem>

                                        <asp:ListItem Value="10002"></asp:ListItem>
                                        <asp:ListItem Value="acceptappletter"></asp:ListItem>


                                      <%--  <asp:ListItem Value="10003"></asp:ListItem>--%>
                      
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                        </div>
                          </div>
                         </div>
                         <div class="col-9">
                             
                 
           
                            <div class="table table-sm table-responsive">
                                <asp:Label runat="server" ID="lbladvnoo" Visible="false"></asp:Label>
                                <asp:GridView CssClass=" table-striped table-hover table-bordered" ID="gvAllRec" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvAllRec_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbladvno" runat="server" Text='<%#Eval("advno").ToString()%>' Width="10px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblname" runat="server" Text='<%#Eval("name").ToString()%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
             
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldesig" runat="server" Text='<%#Eval("desig").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Mobile">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmobile" runat="server" Text='<%#Eval("mobile").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lblemail" runat="server" Text='<%#Eval("email").ToString()%>' Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Present Address" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpereadd" runat="server" Text='<%#Eval("peradd").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Present Address" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpreadd" runat="server" Text='<%#Eval("preadd").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Department" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldept" runat="server" Text='<%#Eval("dept").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                    
                                        </asp:TemplateField>

                                        
                                <%--        <asp:TemplateField HeaderText="Type" >
                                            <ItemTemplate>  
                                                <span class="badge badge-info" runat="server" id="lbltype"></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                          <asp:TemplateField HeaderText="Status" >
                                            <ItemTemplate>  
                                                <%--<span class="badge badge-info" runat="server" id="lblstatus"> </span>--%>
                                                  <asp:Label ID="lblstatus" runat="server" Text='<%#Eval("sendappflag").ToString()%>' Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Operation">
                                            <ItemTemplate>

                                                       <asp:LinkButton runat="server" ID="lnkAccept" OnClick="lnkAccept_Click" ToolTip="Accept Letter" Visible="false">
                                                <i class="fa fa-check"></i>
                                        
                                                </asp:LinkButton>
                                                <asp:LinkButton runat="server" ID="RejectLetter" OnClick="RejectLetter_Click" ToolTip="Reject Letter" Visible="false"
                                                     CssClass='<%#(Convert.ToString(DataBinder.Eval(Container.DataItem, "isreject"))=="False") ? " text-danger active ": " disabled " %>'>  
                                                
                                                  <%--           <span><%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "isreject"))=="False")?"Reject":"Rejected "%></span>--%>
                                                    <i class="fa fa-ban"></i>
                                                </asp:LinkButton>
                                                <asp:HyperLink  runat="server"  ID="lnkOfferLetter" Target="_blank" CssClass="text-info" ToolTip="Offer Letter"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10003 &Page=NewRec &Entry=offer Letter &advno="+Eval("advno") %>'
                                                                > <i class="fa fa-envelope"></i></asp:HyperLink>

                                                <asp:HyperLink runat="server" ID="lnkAppoint" Target="_blank" ToolTip="Appointment Letter"
                                                                NavigateUrl='<%# "~/LetterDefault?Type=10002 &Page=NewRec &Entry=appoinment Letter &advno="+Eval("advno") %>'
                                                                 CssClass="text-primary"> <i class="fa fa-envelope"></i></asp:HyperLink>

                                                 <asp:HyperLink  runat="server" ID="lnkConfirmation" Target="_blank"
                                                                           NavigateUrl='<%# "~/LetterDefault?Type=10025 &Page=NewRec &Entry=confirmation Letter &advno="+Eval("advno") %>'
                                                                           CssClass="btn btn-success btn-sm">Confirmation Letter</asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
            
                 
                         </div>
                         </div>
                     </div>
                     </div>

         

        </ContentTemplate>
    </asp:UpdatePanel>





</asp:Content>
