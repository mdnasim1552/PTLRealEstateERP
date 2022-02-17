<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="WorkCodeLink.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.WorkCodeLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">




        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {

            try {

                $("input, select").bind("keydown", function (event) {
                    var k1 = new KeyPress();
                    k1.textBoxHandler(event);

                });

                $('.chzn-select').chosen({ search_contains: true });
            }
            catch (e) {
                alert(e)
            }


        }








    </script>



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

            <div class="card card-fluid" style="min-height: 550px;">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="ddlUserName">User Name</label>
                                <asp:DropDownList ID="ddlUserList" runat="server" CssClass="custom-select chzn-select">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label d-block" for="ddlUserName">&nbsp; </label>
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass=" btn btn-primary btn-xs" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        
                        <div class="col-md-1">
                            <div class="colMdbtn pading5px">
                                <asp:Label ID="lblmsg1" CssClass="btn-danger btn disabled primaryBtn" runat="server" Visible="false"></asp:Label>
                            </div>

                        </div>


                    </div>

                    <asp:Panel ID="Panel2" runat="server" Visible="False">

                        <div class="row">
                            
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label d-block" for="ddlUserName">
                                        <asp:LinkButton ID="ImgbtnFindProject" class="p-0" runat="server" OnClick="ImgbtnFindProject_Click"> Work List</asp:LinkButton></label>
                                    <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select chzn-select">
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label d-block" for="ddlUserName">&nbsp; </label>
                                    <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass=" btn btn-primary btn-xs" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                                </div>
                            </div>



                        </div>








                        <asp:GridView ID="gvCALinkInfo" runat="server" CssClass=" table-condensed table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                            OnRowDeleting="gvCALinkInfo_RowDeleting">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="user Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod0" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" DeleteText="" ControlStyle-CssClass="fa fa-times text-red btn-xs" />

                                <asp:TemplateField HeaderText="cacode Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvprocode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cacode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvproDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cadesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnSuplUpdate" runat="server" OnClick="lbtnSuplUpdate_Click"
                                            CssClass="btn  btn-danger primarygrdBtn">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                            Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <RowStyle CssClass="grvRows" />
                        </asp:GridView>


                    </asp:Panel>




                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


