<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="ProjectGroup.aspx.cs" Inherits="RealERPWEB.F_22_Sal.ProjectGroup" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            try {
                $('.chzn-select').chosen({ search_contains: true });
            }

            catch (e) {

            }

        };
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <fieldset class="contentPart contentPartSmall">


                   <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                 <div class="form-group">

                                    <asp:Label ID="lblgroup" runat="server" CssClass="lblTxt lblName" Text="Group:"></asp:Label>

                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlprjgroup" runat="server" CssClass=" chzn-select form-control inputTxt" Width="240px" OnSelectedIndexChanged="ddlprjgroup_SelectedIndexChanged" AutoPostBack="true">
                                        </asp:DropDownList>

                                        

                                    </div>
                                    <div class="col-md-1 pading5px" style="margin-left: -50px;">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkOk_OnClick">Ok</asp:LinkButton> 
                                    </div>
                                    
                                   
                                </div>
                                 <div class="form-group">
                                         <asp:Label ID="lblprj" runat="server" CssClass="lblTxt lblName" Text="Project:"></asp:Label>
                                     
                                     
                                     
                                     <cc1:DropCheck ID="DropCheck1" runat="server" BackColor="Black" CssClass=" " style="float: left;"
                                    MaxDropDownHeight="240" TabIndex="8" TransitionalMode="True" Width="240px"></cc1:DropCheck>
                                      
                                        <div class="col-md-1 pading5px">
                                            <asp:LinkButton ID="lnk_add" style="margin-left: 240px;margin-top: -20px;" runat="server" CssClass="btn btn-primary okBtn" TabIndex="2" OnClick="lnk_add_OnClick">Add</asp:LinkButton>

                                        </div>
                                    </div>

                                </div>
                       </fieldset>
                        
                              

                    <div class="table-responsive">
                        <ASP:GridView runat="server" ID="gvProject" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Style="text-align: left">
                            <RowStyle/>
                            <Columns>
                                <asp:TemplateField HeaderText="S.L">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Width="30px" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' ></asp:Label>
                                    </ItemTemplate>
                                    
                                </asp:TemplateField>
                                <asp:TemplateField>
                                     <ItemTemplate>
                                        <asp:LinkButton runat="server" id="lnkdel" OnClick="lnkdel_OnClick" ToolTip="Delete"><span class="btn-danger glyphicon glyphicon-trash"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Project Name">
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblactcode" Visible="False" Width="300px" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem,"actcode")) %>'></asp:Label>
                                        
                                        <asp:Label runat="server" Width="300px" Text='<%#Convert.ToString(DataBinder.Eval(Container.DataItem,"actdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton runat="server" id="lnk_update" OnClick="lnk_update_OnClick" CssClass="btn btn-xs btn-danger">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    
                                    <ItemStyle Font-Size="11px" />
                                    
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#000" />
                                </asp:TemplateField>
                            </Columns>
                        </ASP:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

