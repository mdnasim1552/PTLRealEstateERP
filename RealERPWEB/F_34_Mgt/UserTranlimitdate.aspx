<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="UserTranlimitdate.aspx.cs" Inherits="RealERPWEB.F_34_Mgt.UserTranlimitdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<script type="text/javascript">

    $(document).ready(function () {

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

    });
    function pageLoaded() {

      

        $('.chzn-select').chosen({ search_contains: true });

      
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

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <div class="col-md-12">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">



                                      <div class="col-md-2 pading5px ">
                                        
                                        <asp:Label ID="lblDate" runat="server" CssClass="smLbl" Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                                      <asp:Label ID="lblUser" runat="server" CssClass="smLbl" Text="User"></asp:Label>

                                    </div>
                                     

                                        <div class="col-md-4 pading5px ">                                            
                                            <asp:DropDownList ID="ddlUserName" runat="server" CssClass=" chzn-select  form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>


                                        <div class="col-md-1 padding5px">

                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lbtnOk0" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            </div>

                                        </div>

                                    </div>


                                </div>
                            </fieldset>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <asp:GridView ID="gvcomlimit" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvcomlimit_RowDataBound">
                            <RowStyle />
                            <Columns>


                                <asp:TemplateField HeaderText="Company Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblcompany" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "company")) %>'
                                            Width="150px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>





                                <asp:TemplateField HeaderText="Time">


                                      <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server"  CssClass="btn btn-danger btn-xs"
                                          OnClick="lbtnUpdate_Click"
                                            Style="text-decaration: none;">Update</asp:LinkButton>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlhourPart" runat="server">
                                        </asp:DropDownList>
                                        :
                                         <asp:DropDownList ID="ddlminpart" runat="server">
                                         </asp:DropDownList>

                                        <asp:DropDownList ID="ddlampmpart" runat="server">
                                            <asp:ListItem Value="PM">PM</asp:ListItem>
                                            <asp:ListItem Value="AM">AM</asp:ListItem>
                                        </asp:DropDownList>


                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>



                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle BackColor="#5F9467" ForeColor="#ffffff" />
                        </asp:GridView>
                    </div>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





