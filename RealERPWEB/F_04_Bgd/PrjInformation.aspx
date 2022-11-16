<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PrjInformation.aspx.cs" Inherits="RealERPWEB.F_04_Bgd.PrjInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });


        });

        function pageLoaded(parameters) {

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
            <div class="card mt-4 ">
                <div class="card-body">
                    <div class="row mb-4">
                      

                                
                                    <div class="col-md-3 pading5px asitCol3 d-none">
                                        <asp:Label ID="lblProjectName" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="inputtextbox"></asp:TextBox>



                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass=" btn btn-primary srearchBtn" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="col-md-4 pading5px">
                                        <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="chzn-select form-control inputTxt">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblProjectdesc" runat="server" CssClass="form-control inputTxt" Visible="false"></asp:Label>



                                    </div>

                                    <div class="col-md-1 ml-4">

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            <asp:LinkButton ID="lbtnEnglish" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnEnglish_Click" Visible="false">Bangla</asp:LinkButton>

                                        </div>

                                    </div>

                                    <div class="progress progress-bar-danger" style="display: none;">
                                        <div class="progress-bar progress-bar-striped active" role="progressbar"
                                            aria-valuenow="40" aria-valuemin="0" aria-valuemax="100" style="width: 40%">
                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px pull-right d-none">

                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg" CssClass="btn  btn-success primarygrdBtn" runat="server" Visible="false"></asp:Label>



                                        </div>
                                    </div>
                                
                          

                    </div>
                    </div>
                </div>

             <div class="card" style="min-height:500px">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-8">
                            <asp:GridView ID="gvPrjInfo" runat="server" AutoGenerateColumns="False"
                                CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True" Width="430px">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
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
                                    <asp:TemplateField HeaderText="Unit">

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtResunit" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gunit")) %>'
                                                Width="50px" Font-Size="11px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lUpdatPerInfo" runat="server" OnClick="lUpdatPerInfo_Click" CssClass="btn btn-danger primaryBtn">Update Information</asp:LinkButton>

                                        </FooterTemplate>

                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Width="150px" Style="text-align: left"></asp:TextBox>


                                            <asp:TextBox
                                                ID="txtgvdVal" runat="server" BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                Width="150"></asp:TextBox>

                                            <cc1:CalendarExtender ID="CalendarExtender_txtgvdVal" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>


                                            <asp:DropDownList ID="ddlcataloc" runat="server" CssClass=" chzn-select form-control" Width="150px" AutoPostBack="true"></asp:DropDownList>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                </Columns>
                                 <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <RowStyle CssClass="grvRows" />
                            </asp:GridView>
                        </div>
                        <div class="col-md-4" id="imgpanel" runat="server" visible="false">

                            <div class="row-fluid">

                                <div class="panel panel-primary">
                                    <div class="panel-heading">
                                        <span class="glyphicon glyphicon-picture"></span>Image/Map Upload
                                    <div class="pull-right">
                                        <div class=" file-upload">

                                            <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                OnClientUploadComplete="uploadComplete" runat="server"
                                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                ThrobberID="imgLoader"
                                                OnUploadedComplete="FileUploadComplete" />
                                            <%--<span class="glyphicon glyphicon-upload">
                                                <asp:FileUpload runat="server" OnClientUploadError="uploadError" ID="AsyncFileUpload1"
                                                    OnClientUploadComplete="uploadComplete" OnUploadedComplete="FileUploadComplete" />
                                            </span>--%>


                                            <asp:Image ID="imgLoader" runat="server" Visible="False" ImageUrl="~/images/Wait.gif" />
                                            <br />
                                        </div>
                                        <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" OnClientClick="return confirm('Really Do You want to Delete This Image?')" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>
                                    </div>
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="row">
                                                    <div class="form-group">
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                </div>

                                            </div>

                                        </div>
                                        <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                            <LayoutTemplate>
                                                <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                            </LayoutTemplate>
                                            <ItemTemplate>
                                                <div class="col-xs-12 col-sm-4 col-md-4 listDiv" style="padding: 0 5px;">
                                                    <div id="EmpAll" runat="server">

                                                        <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("imgpath") %>'></asp:Label>
                                                        <asp:Label ID="actcode" Visible="false" runat="server" Text='<%# Eval("actcode") %>'></asp:Label>

                                                        <a href="#" class="uploadedimg">

                                                            <asp:Image ID="GetImg" runat="server" CssClass="pop image img img-responsive img-thumbnail " Height="65px" />
                                                            <div class="middle">
                                                                <%--   <span><%# Eval("pactcode") %></span>--%>
                                                            </div>
                                                            <div class="checkboxcls">
                                                                <asp:CheckBox ID="ChDel" Style="margin: 0px 80px; padding: 0px;" runat="server" />
                                                            </div>
                                                        </a>
                                                    </div>
                                                </div>
                                            </ItemTemplate>
                                        </asp:ListView>
                                        <div class="clearfix"></div>
                                        <br />
                                        <div class="row">

                                            <div class="col-lg-12">
                                                <asp:HyperLink CssClass="btn btn-sm btn-success" ID="GoogleMap" runat="server">Google Map <span class="glyphicon glyphicon-map-marker"></span></asp:HyperLink>
                                                <asp:HyperLink CssClass="btn btn-sm btn-success" ID="PurchaseMap" runat="server">Purchase Map <span class="glyphicon glyphicon-globe"></span></asp:HyperLink>
                                                <asp:HyperLink CssClass="btn btn-sm btn-success" ID="combinedMap" runat="server">CS and RS Map <span class="glyphicon glyphicon-globe"></span></asp:HyperLink>
                                                <asp:HyperLink CssClass="btn btn-sm btn-success" ID="SalesMap" runat="server">Sales Map <span class="glyphicon glyphicon-globe"></span></asp:HyperLink>

                                            </div>
                                        </div>
                                    </div>
                                </div>


                            </div>



                        </div>


                        <div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                            <div class="modal-dialog">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                                        <h4 class="modal-title" id="myModalLabel">Project Image preview</h4>
                                    </div>
                                    <div class="modal-body">
                                        <img src="" id="imagepreview" class="img img-responsive">
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- End of contentpart-->

            </div>
            <!-- End of Container-->


        </ContentTemplate>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            $(".pop").on("click", function () {
                $('#imagepreview').attr('src', $(this).attr('src')); // here asign the image to the modal when the user click the enlarge link
                $('#imagemodal').modal('show'); // imagemodal is the id attribute assigned to the bootstrap modal, then i use the show function
            });
        }
    </script>

    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
</asp:Content>
