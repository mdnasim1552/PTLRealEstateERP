<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjImgUpload.aspx.cs" Inherits="RealERPWEB.F_33_Doc.ProjImgUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">

                <fieldset class="scheduler-border fieldset_A">
                    <div class="col-md-12">
                        <div class="form-horizontal">


                            <fieldset class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt col-md-1 lblName">Select Project</asp:Label>
                                <div class="col-md-3 pading5px ">
                                    <asp:DropDownList ID="ddlPrjName" runat="server" CssClass="form-control inputTxt">
                                    </asp:DropDownList>

                                </div>
                                <asp:Label ID="lbldate" runat="server" CssClass="lblTxt col-md-1 lblName">Date</asp:Label>
                                <div class="col-md-1 ">
                                    <asp:TextBox ID="txtDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDate"></cc1:CalendarExtender>

                                </div>
                                <div class="col-md-3">
                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                        OnClientUploadComplete="uploadComplete" runat="server"
                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                        CompleteBackColor="White" CssClass="pading5px form-control"
                                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                        OnUploadedComplete="FileUploadComplete" />
                                    <asp:Image ID="imgLoader" runat="server" ImageUrl="~/images/Wait.gif" />
                                </div>
                                <div class="col-md-2">

                                    <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs pull-left" Text="Show" OnClick="btnShowimg_Click" />
                                    &nbsp;
                                       <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" OnClientClick="return confirm('Really Do You want to Delete This Image?')" Visible="False" CssClass=" btn btn-xs btn-danger">DELETE</asp:LinkButton>

                                </div>


                                <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>


                            </fieldset>

                            <%--  <div class="carousel-inner" id="TrendingdataData" runat="server">
                                </div>--%>

                            <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                <LayoutTemplate>
                                    <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                </LayoutTemplate>
                                 <ItemTemplate>
                                            <div class="col-xs-6 col-sm-2 col-md-2 listDiv" style="padding: 0 5px;">
                                                <div id="EmpAll" runat="server">

                                                    <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("url") %>'></asp:Label>
                                                    <asp:Label ID="procode" Visible="false" runat="server" Text='<%# Eval("procode") %>'></asp:Label>

                                                    <a href="<%#this.ResolveUrl( Convert.ToString(DataBinder.Eval(Container.DataItem, "url")))%>" target="_blank" class="uploadedimg">

                                                        <asp:Image ID="GetImg" runat="server" CssClass="pop image img img-responsive img-thumbnail " Height="135px" />
                                                       
                                                        <div class="checkboxcls">
                                                            <asp:CheckBox ID="ChDel" Style="margin: 0px 80px; padding: 0px;" runat="server" />
                                                        </div>
                                                    </a>
                                                </div>
                                            </div>
                                        </ItemTemplate>

                                <%--<ItemTemplate>
                                    <asp:Label runat="server" Visible="False" ID="lblprocode" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'></asp:Label>
                                    <asp:Label runat="server" Visible="False" ID="filepath" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "filePath")) %>'></asp:Label>

                                    <div class="col-xs-12 col-sm-2 col-md-2 listDiv" style="padding: 0 5px;">
                                        <div id="EmpAll" runat="server">

                                            <div style="margin-bottom: 2px; padding-bottom: 20px">
                                                <a href="#" id="">
                                                    <asp:CheckBox ID="ChDel" runat="server" />
                                                    <img class="pop img img-thumbnail" src="<%# Eval("filePath1") %>" style="width: 150px; height: 150px;">
                                                </a>

                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </div>
                                </ItemTemplate>--%>
                            </asp:ListView>

                        </div>
                        <div class="row">
                        </div>
                    </div>

                </fieldset>

            </div>

        </div>


        <!---  for modal--->


        <!-- Creates the bootstrap modal where the image will appear -->
        <%--<div class="modal fade" id="imagemodal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>
                        <h4 class="modal-title" id="myModalLabel">Project Image preview</h4>
                    </div>
                    <div class="modal-body">
                        <img src="" id="imagepreview" class="img img-responsive" alt="">
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>--%>
        <!--end--->
    </div>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
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



