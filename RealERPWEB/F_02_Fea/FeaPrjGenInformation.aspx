<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="FeaPrjGenInformation.aspx.cs" Inherits="RealERPWEB.F_02_Fea.FeaPrjGenInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


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

        }
    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">

                                            <asp:Label ID="lblItem" runat="server" CssClass="lblTxt lblName">Project Name</asp:Label>

                                            <asp:TextBox ID="txtSrcPro" AutoCompleteType="Disabled" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="1"></asp:TextBox>
                                            <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindProject_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblProjectdesc" runat="server" CssClass=" form-control inputTxt" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlPrjName" runat="server" CssClass=" form-control inputTxt">
                                            </asp:DropDownList>

                                        </div>

                                        <div class="col-md-3 pading5px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn"
                                                OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            <asp:Label ID="lblMsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>
                    <div class="table table-responsive">
                        <asp:GridView ID="gvProjectInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="772px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgcod")) %>'
                                            Width="49px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lgcResDesc1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc")) %>'
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
                                            Width="4px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvgval" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgval")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lUpdatProInfo" runat="server"  OnClick="lUpdatProInfo_Click" CssClass="btn btn-danger primaryBtn"
                                            >Update Information</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvVal" runat="server" CssClass="form-control inputTxt" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prgdesc1")) %>'
                                            Width="510px"></asp:TextBox>
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


            
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>






