<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="PurSupplierinfo.aspx.cs" Inherits="RealERPWEB.F_14_Pro.PurSupplierinfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });

            $(".chkmobile").keyup(function () {
                var $this = $(this);
                $this.val($this.val().replace(/[^\d.]/g, ''));
            });
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
            <div class="row mt-5" runat="server">
                <div class="col-lg-12">
                    <div class="card">
                        <div class="card-header bg-light"><span class="font-weight-bold text-muted">Supplier/Sub-Contractor Information- Entry</span></div>

                        <div class="card-body" runat="server">
                            <div class="row">
                                <div class="col-lg-3">
                                    <label for="ddlLvType" class="d-block">
                                        Supplier    <asp:LinkButton ID="ibtnFindProject" OnClick="ibtnFindProject_Click" runat="server"><i class="fa fa-search"> </i></asp:LinkButton>
                                    </label>
                                    <asp:DropDownList ID="ddlSName" runat="server"  CssClass="chzn-select form-control" TabIndex="1">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-lg-1"> 
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click" Style="margin-top: 28px;" TabIndex="2">Ok</asp:LinkButton>
                                     
                                </div>
                            </div>
                            <div class="row  mt-2">
                                <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" Width="831px" OnRowDataBound="gvPersonalInfo_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl#">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:Label ID="lgp" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                    Width="2px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Type" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger primaryBtn  btn-xs" OnClick="lUpdatPerInfo_Click">Update Information</asp:LinkButton>


                                                <asp:LinkButton ID="lnkMesSend" runat="server" OnClick="lnkMesSend_Click" CssClass="btn btn-success btn-xs okbtn pull-right">Send SMS</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent" CssClass="form-control"
                                                      Height="30px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                     ></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>


                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
             
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>




