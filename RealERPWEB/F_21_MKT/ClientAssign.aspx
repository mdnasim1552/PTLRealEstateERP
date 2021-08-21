<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ClientAssign.aspx.cs" Inherits="RealERPWEB.F_21_MKT.ClientAssign" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style>
    #AsyncFileUpload1 input
        {
            position: absolute;
            top: 0;
            right: 0;
            margin: 0;
            border: solid transparent;
            border-width: 0 0 100px 200px;
            opacity: 0.0;
            filter: alpha(opacity=0);
            -o-transform: translate(250px, -50px) scale(1);
            -moz-transform: translate(-300px, 0) scale(4);
            direction: ltr;
            cursor: pointer;
        }
        </style>
    <script type="text/javascript" language="javascript">
        $(document)
            .ready(function () {
                Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });

        }
        function openModal() {
            //    $('#myModal').modal('show');
            $('#myModal').modal('toggle');
        }
          function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }

        function ClientDisPage(page) {
          
           
            window.open(page, '_blank');
        }

        function CloseModal()
        {
            $('#myModal').modal('hide');

        }



    </script>
    <asp:UpdatePanel ID="uppnlclint" runat="server">
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
            <div class="container moduleItemWrpper">
                <asp:Panel ID="pnlAssign" Visible="false" runat="server">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-4">
                                        <asp:Label ID="lblRef" runat="server" CssClass="lblTxt lblName">Employee Name :</asp:Label>
                                        <asp:DropDownList ID="ddlEmplist" runat="server" Width="250" CssClass="form-control inputTxt pull-left chzn-select">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-md-1" style="margin-left: -30px">
                                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </fieldset>
                    </div>
                </asp:Panel>
                <div class="row">
                    <asp:GridView ID="gvAdDetails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvAdDetails_RowDataBound"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea col-md-10">
                        <RowStyle />
                        <Columns>

                            <asp:TemplateField HeaderText="S.L">
                                <ItemTemplate>
                                    <asp:Label ID="serialno" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Client Id">

                                <ItemTemplate>
                                    <asp:Label ID="serialnoid0" runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>' Width="50px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtclname" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                        Width="120px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Mobile">
                                <ItemTemplate>
                                    <asp:Label ID="txtclmob" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mob")) %>'
                                        Width="70px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdate_OnClick">Update</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Email">
                                <ItemTemplate>
                                    <asp:Label ID="txtclemail"
                                        runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "email")) %>'
                                        Width="110px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Information">
                                <ItemTemplate>
                                    <asp:Label ID="txtclinfo"
                                        runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "info")) %>'
                                        Width="140px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:Label>

                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Location">
                                <ItemTemplate>
                                    <asp:Label ID="lbllocat" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locat")) %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Profession">
                                <ItemTemplate>
                                    <asp:Label ID="lblpro" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pro")) %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None"></asp:Label>

                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Flat Size">
                                <ItemTemplate>
                                    <asp:Label ID="txtclsize" runat="server" Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "size")).ToString("#,##0.00;-#,##0.00; ") %>'
                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtclrmks" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rmks")) %>'
                                        Width="150px" BackColor="Transparent" BorderStyle="Groove"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="txtclrdmks" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clstatus")) %>'
                                        Width="80px" BackColor="Transparent" BorderStyle="None"></asp:Label>


                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>

                                    <asp:CheckBox ID="chkempid" runat="server"
                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                        Enabled='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True")?false:true %>'
                                        Width="20px" />


                                    <asp:LinkButton ID="lnkAddclient" Width="100" Visible="false" runat="server" OnClick="lnkAddclient_Click"><span class="glyphicon glyphicon-plus"></span>Add Client</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="150" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>


                <div class="row">

                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="contactLabel" aria-hidden="true">
                        <div class="modal-dialog modal-md">
                            <div class="panel panel-primary">
                                <div class="panel-heading">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                                    <h4 class="panel-title" id="contactLabel"><span class="glyphicon glyphicon-info-sign"></span>Add New Client</h4>
                                </div>
                                
                                    <div class="modal-body" style="padding: 5px;">
                                        <div class="row">

                                            <asp:DropDownList ID="ddlprofession" CssClass="form-control" runat="server" Visible="false">
                                            </asp:DropDownList>

                                            <div class="col-md-12">
                                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                                    ShowFooter="True"
                                                    OnRowDataBound="gvPersonalInfo_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">

                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sl.No.">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                                    Style="text-align: right"
                                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"
                                                                    ForeColor="Black"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                                    Width="90px" ForeColor="Black"></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Description of Item">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                                    Width="150px" ForeColor="Black"></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Type" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lgvgval" runat="server"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Item Value">

                                                            <ItemTemplate>

                                                                <asp:TextBox ID="txtgvValMob" runat="server"   Visible="false" BackColor="Transparent" BorderStyle="None"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                    Width="350px"></asp:TextBox>

                                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                                    Width="350px"></asp:TextBox>
                                                                <asp:DropDownList ID="ddlprofession" CssClass="form-control" runat="server" Visible="false">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtgvCal" runat="server" Visible="false" BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'></asp:TextBox>

                                                                <cc1:calendarextender id="txtPublish_CalendarExtender" runat="server" format="dd-MMM-yyyy" targetcontrolid="txtgvCal"></cc1:calendarextender>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        </asp:TemplateField>


                                                    </Columns>
                                                    <FooterStyle BackColor="#F5F5F5" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>
                                            </div>
                                            <div class="col-md-12">

                                                <div class=" file-upload">

                                                    <cc1:asyncfileupload onclientuploaderror="uploadError"
                                                        onclientuploadcomplete="uploadComplete" runat="server"
                                                        id="AsyncFileUpload1" uploaderstyle="Modern"
                                                        completebackcolor="White" tooltip="Browse File"
                                                        uploadingbackcolor="#CCFFFF" throbberid="imgLoader"
                                                        onuploadedcomplete="FileUploadComplete" width="250px" />
                                                    <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/uploadnahid.gif" />
                                                    <br />
                                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>

                                                </div>





                                            </div>


                                        </div>
                                    </div>
                                    <div class="panel-footer" style="margin-bottom: -14px;">
                                        <button style="float: right;" type="button" class="btn btn-default btn-close" data-dismiss="modal">Close</button>

                                        <asp:LinkButton ID="lUpdatPerInfo" OnClientClick="CloseModal();" Style="float: right; margin-right: 10px;" runat="server" class="btn btn-success" OnClick="lUpdatPerInfo_Click" >Save</asp:LinkButton>
                                        <div class="clearfix"></div>
                                        <!--<span class="glyphicon glyphicon-remove"></span>-->
                                    </div>
                             
                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

