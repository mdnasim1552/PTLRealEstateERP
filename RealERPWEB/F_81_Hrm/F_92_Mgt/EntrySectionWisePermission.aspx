<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="EntrySectionWisePermission.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.EntrySectionWisePermission" %>
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
                                 <label class="control-label d-block" for="ddlUserName"> &nbsp; </label>
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
                                    <label class="control-label d-block" for="ddlUserName">   <asp:LinkButton ID="ImgbtnFindProject"  class="p-0"  runat="server" OnClick="ImgbtnFindProject_Click">Compnay Name</asp:LinkButton></label>
                                 
                                    <%--<label class="control-label" for="ddlUserName">Project Code</label>--%>
                                 <%--   <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select chzn-select">--%>
                                            <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" TabIndex="2">

                                    </asp:DropDownList>

                                </div>
                            </div>
                           
                            </div>

                             <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label d-block" for="ddlUserName">   <asp:LinkButton ID="LinkButton1"  class="p-0"  runat="server" OnClick="ImgbtnFindProject_Click">Department Name</asp:LinkButton></label>
                                 
                                    <%--<label class="control-label" for="ddlUserName">Project Code</label>--%>
                                 <%--   <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select chzn-select">--%>
                                            <asp:DropDownList ID="ddlDeptName" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptName_SelectedIndexChanged" TabIndex="2">

                                    </asp:DropDownList>

                                </div>
                            </div>
                                 </div>
                            

                            
                          <div class="row">

                            <div class="col-md-3">
                                <div class="form-group">
                                    <label class="control-label d-block" for="ddlUserName">   <asp:LinkButton ID="LinkButton4"  class="p-0"  runat="server" OnClick="ImgbtnFindProject_Click">Section Name</asp:LinkButton></label>
                                 
                                    <%--<label class="control-label" for="ddlUserName">Project Code</label>--%>
                                 <%--   <asp:DropDownList ID="ddlProjectName" runat="server" CssClass="custom-select chzn-select">--%>
                                            <asp:DropDownList ID="ddlSection" runat="server" Width="233" CssClass="form-control inputTxt pull-left" AutoPostBack="true" TabIndex="2">

                                    </asp:DropDownList>

                                </div>
                            </div>
                               <div class="col-md-2">
                                <div class="form-group">
                                    <label class="control-label d-block" for="ddlUserName"> &nbsp; </label>
                                    <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary btn-xs" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass=" btn btn-primary btn-xs" OnClick="lbtnSelectAll_Click">Select All</asp:LinkButton>

                                </div>
                            

                 </div>

                     

                            

                        </div>
                           
                           


                <%--<div class="col-md-3 pading5px asitCol3">
                                    <asp:Label ID="lblConTrolCode" runat="server" CssClass="lblTxt lblName"></asp:Label>
                                    <asp:TextBox ID="txtProSearch" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindProject" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ImgbtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                </div>--%>
                <%-- <div class="col-md-4 pading5px ">
                                    <asp:DropDownList ID="" runat="server" CssClass="  chzn-select  form-control inputTxt">
                                    </asp:DropDownList>
                                </div>



                                <div class="col-md-1 pading5px">

                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="" runat="server" Style="margin: 0;" CssClass="btn btn-primary primaryBtn checkbox" OnClick=""></asp:LinkButton>
                                    </div>

                                </div>
                                <div class="col-md-1 pading5px">
                                    <div class="colMdbtn pading5px">
                                        <asp:LinkButton ID="" runat="server" Style="margin: 0;" CssClass="btn btn-primary  primaryBtn checkbox" OnClick="">Select All</asp:LinkButton>

                                    </div>

                                </div>--%>




                <asp:GridView ID="gvProLinkInfo" runat="server" CssClass=" table-condensed table-hover table-bordered grvContentarea"
                    AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                    OnRowDeleting="gvProLinkInfo_RowDeleting">
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
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>

                        <asp:CommandField ShowDeleteButton="True" DeleteText=""  ControlStyle-CssClass="fa fa-times text-red btn-xs" />

                        <asp:TemplateField HeaderText="pactcode Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblgvprocode" runat="server" Height="16px"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "code")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Project Name">

                            <ItemTemplate>
                                <asp:Label ID="lblgvproDesc" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sectiondesc")) %>'
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

