<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="SaleSurPrjInformation.aspx.cs" Inherits="RealERPWEB.F_22_Sal.SaleSurPrjInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

  

        <script language="javascript" type="text/javascript">

            $(document).ready(function () {
              
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                    });




            function pageLoaded()
            {

                $('.chzn-select').chosen({ search_contains: true });

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });

            }
   



        </script>
    <style>
        .lblHead {
            color: blue;
            font-size: 14px !important;
            font-weight: bold;
        }

        .table {
            margin-bottom: 0;
        }

        .middle {
            transition: .5s ease;
            opacity: 0;
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
        }

        .checkboxcls {
            opacity: 1;
            position: absolute;
            top: 80%;
            left: 10%;
        }

        .uploadedimg .image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;
        }

        .uploadedimg:hover .image {
            opacity: 0.3;
        }

        .uploadedimg:hover .middle {
            opacity: 1;
        }

        .file-upload {
            display: inline-block;
            overflow: hidden;
            text-align: center;
            vertical-align: middle;
            font-family: Arial;
            border: 1px solid #124d77;
            background: #007dc1;
            color: #fff;
            border-radius: 6px;
            -moz-border-radius: 6px;
            cursor: pointer;
            text-shadow: #000 1px 1px 2px;
            -webkit-border-radius: 6px;
        }

            .file-upload:hover {
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #0061a7), color-stop(1, #007dc1));
                background: -moz-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -webkit-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -o-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: -ms-linear-gradient(top, #0061a7 5%, #007dc1 100%);
                background: linear-gradient(to bottom, #0061a7 5%, #007dc1 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#0061a7', endColorstr='#007dc1',GradientType=0);
                background-color: #0061a7;
            }

        /* The button size */
        .file-upload {
            height: 30px;
        }

            .file-upload, .file-upload span {
                width: 50px;
            }

                .file-upload input {
                    top: 0;
                    left: 0;
                    margin: 0;
                    font-size: 11px;
                    font-weight: bold;
                    /* Loses tab index in webkit if width is set to 0 */
                    /*opacity: 0;
            filter: alpha(opacity=0);*/
                }

                .file-upload strong {
                    font: normal 12px Tahoma,sans-serif;
                    text-align: center;
                    vertical-align: middle;
                }

                .file-upload span {
                    top: 0;
                    left: 0;
                    display: inline-block;
                    /* Adjust button text vertical alignment */
                    padding-top: 5px;
                }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="RealProgressbar">
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
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">


                        <div class="col-md-1">
                            <div class="form-group">
                                <label for="Label5" runat="server" class=" control-label  lblmargin-top9px ">Project Name</label>


                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control chzn-select" TabIndex="3">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>




                    </div>

                    <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False"
                        CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="430px">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                </ItemTemplate>

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                        Width="49px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <FooterTemplate>
                                    <%-- <asp:HyperLink ID="HypMapLink" Target="_blank" runat="server" CssClass="btn btn-xs btn-success">Map</asp:HyperLink>
                                       <asp:HyperLink ID="HypMapLink2" Target="_blank" runat="server" CssClass="btn btn-xs btn-success">Map New</asp:HyperLink>--%>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgcResDesc1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                        Width="200px" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Type" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvgval" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           

                            <asp:TemplateField>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="lUpdatPerInfo_Click" CssClass="btn btn-danger primaryBtn">Final Update</asp:LinkButton>

                                </FooterTemplate>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                        Width="180px" Style="text-align: left"></asp:TextBox>


                                    <asp:TextBox
                                        ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                        Width="180"></asp:TextBox>

                                    <cc1:CalendarExtender ID="CalendarExtender_txtgvdVal" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>


                                    <asp:DropDownList ID="ddlcataloc" runat="server" CssClass=" chzn-select form-control" Width="180px" AutoPostBack="true"></asp:DropDownList>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle  />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="" />
                        <HeaderStyle CssClass=""/>
                    </asp:GridView>

                </div>


            </div>





        </ContentTemplate>
    </asp:UpdatePanel>



   
</asp:Content>
