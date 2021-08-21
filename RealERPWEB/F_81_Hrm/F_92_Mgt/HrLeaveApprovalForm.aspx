<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="HrLeaveApprovalForm.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_92_Mgt.HrLeaveApprovalForm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

   

   <%-- <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css"
        rel="stylesheet" type="text/css" />
    


    <script type="text/javascript">
        $(function () {
            $('#ddlEmploye').multiselect({
                includeSelectAllOption: true
            });
            $('#btnSelected').click(function () {
                var selected = $("#ddlEmploye option:selected");
                var message = "";
                selected.each(function () {
                    message += $(this).text() + " " + $(this).val() + "\n";
                });
                alert(message);
            });
        });
    </script>
    --%>
    
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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-group">
                            <div class="col-md-5 pading5px">

                                <asp:Label ID="LblCentre" runat="server" CssClass="lblTxt lblName">Department</asp:Label>
                               
                                <asp:DropDownList ID="ddldpt" Width="300" runat="server" CssClass="form-control inputTxt chzn-select">
                                </asp:DropDownList>
                            </div>
                                <div class="col-md-1 pading5px asitCol3">
                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnOkOrNew" runat="server" CssClass="btn btn-primary okBtn" OnClick="GetUserInfo" >Ok</asp:LinkButton>
                                </div>

                            </div>
                            </div>
                           <%-- <div class="form-group">

                                <div class="col-md-4 pading5px">

                                <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Employe </asp:Label>
                               
                                <asp:DropDownList ID="ddlEmploye" Width="300" runat="server"   CssClass="form-control inputTxt chzn-select ">
                                </asp:DropDownList>
                            </div>
                            </div>
                            --%>
                            
                        </fieldset>
                        
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                           <fieldset class="scheduler-border fieldset_A">

                            <div class="col-md-4 pading5px ">
                                <asp:Label ID="lblUser1" runat="server" CssClass="lblTxt lblName">User Name</asp:Label>
                               
                                <asp:DropDownList ID="ddlUserList" Width="300"   runat="server" CssClass="form-control inputTxt chzn-select">
                                </asp:DropDownList>

                                
                            </div>

                            <div class="col-md-1 pading5px asitCol3" style="width:60px;" >
                                <div class="colMdbtn pading5px">
                                    <asp:LinkButton ID="lbtnSelect"   Style="margin: 0; width:50px;" runat="server" CssClass="btn btn-primary okBtn" OnClick="Select_Click">Select</asp:LinkButton>
                                   
                                </div>

                            </div>
                             <div class="col-md-1 pading5px asitCol3">
                                <div class="colMdbtn pading5px" >
                                   
                                    <asp:LinkButton ID="lbtnSelectAll"  runat="server" Style="margin: 0;width:80px;" CssClass="btn btn-primary okBtn" OnClick="SelectAll_Click">Select All</asp:LinkButton>
                                </div>

                            </div>
                            <div class="col-md-3 pading5px asitCol3">
                                <div class="colMdbtn pading5px">
                                    <asp:Label ID="lblmsg1" CssClass="btn-danger btn disabled primaryBtn" runat="server"></asp:Label>
                                </div>
                            </div>
                        </fieldset>
                           
                            
                            <asp:GridView ID="gvProLinkInfo" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" ShowFooter="True" 
                                OnRowDeleting="gvProLinkInfo_RowDeleting">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" 
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />

                                     <asp:TemplateField HeaderText="Sl.No." Visible="true">
                                        <ItemTemplate>
                                            <asp:Textbox ID="slno" runat="server" Height="16px" Style="text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                                Width="30px"></asp:Textbox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="User ID" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="userid" runat="server" Height="16px" Style="text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnDeleteAll" runat="server" Font-Bold="True"
                                                Font-Size="13px" Height="16px" OnClick="lbtnDeleteAll_Click"
                                                 Width="100px">Delete All</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                         
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="User NAME">
                                       
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc1" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lbtnUpdate" runat="server" Font-Bold="True"
                                                Font-Size="13px" OnClick="lbtnUpdate_Click"
                                                Style="text-align: center; height: 15px;" Width="150px">Final Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                   <%-- <asp:TemplateField HeaderText="CENTRE NAME">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplDesc2" runat="server" Style="text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        
                                     
                                        <FooterStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                </Columns>
                                 <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            </asp:GridView>
                        </asp:Panel>
                        <%-- <select id="lstFruits" multiple="multiple">
        <option value="1">Mango</option>
        <option value="2">Apple</option>
        <option value="3">Banana</option>
        <option value="4">Guava</option>
        <option value="5">Orange</option>
    </select>--%>
                    </div>
                </div>
            </div>


            
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js"
        type="text/javascript"></script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
