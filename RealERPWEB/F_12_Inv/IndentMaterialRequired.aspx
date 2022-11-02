<%@ Page Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="IndentMaterialRequired.aspx.cs" Inherits="RealERPWEB.F_12_Inv.IndentMaterialRequired" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
            <div>
                <div class="row mt-5" runat="server">
                    <div class="col-12 col-lg-3 col-xl-3">
                        <div class="card">
                            <div class="card-header bg-light"><span class="font-weight-bold text-muted">Indent Material Required- Entry</span></div>
                            <div class="card-body" runat="server">
                                <div class="row">
                                    <div class="form-group pl-0 col-6">
                                        <label for="ddlLvType">
                                            Date  
                                        </label>
                                        <asp:TextBox ID="txtaplydate" runat="server" AutoPostBack="true" ReadOnly="true" class="form-control disabled"></asp:TextBox>
                                        <%--<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                        TargetControlID="txtaplydate"></cc1:CalendarExtender>--%>
                                    </div>
                                    <div class="form-group col-6 pr-0">
                                        <label for="ddlLvType">
                                            ID  
                                        </label>
                                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" ReadOnly="true" class="form-control disabled"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Department <span class="text-danger">*</span>
                                    </label>
                                    <asp:DropDownList ID="ddlDepartment" OnSelectedIndexChanged="ddlDepartment_SelectedIndexChanged" AutoPostBack="true" class="custom-select chzn-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Materials
                                    </label>
                                    <asp:DropDownList ID="ddlMaterials" OnSelectedIndexChanged="ddlMaterials_SelectedIndexChanged" AutoPostBack="true" class="custom-select chzn-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="ddlLvType">
                                        Specification
                                    </label>
                                    <asp:DropDownList ID="ddlResSpcf" OnSelectedIndexChanged="ddlResSpcf_SelectedIndexChanged" AutoPostBack="true" class="custom-select chzn-select" runat="server"></asp:DropDownList>
                                </div>
                                <div class="form-group text-right">
                                    <asp:LinkButton ID="btnAdd" runat="server" class=" pr-1 btn btn-sm btn-primary" OnClick="btnAdd_Click"><i class="fa fa-plus-circle"></i> Add</asp:LinkButton>

                                </div>
                            </div>


                        </div>
                    </div>
                    <div class="col-12 col-lg-9 col-xl-9">
                        <div class="card">
                            <div class="card-header bg-light"><span class="font-weight-bold text-muted">Added Material's</span></div>
                            <div class="card-body" runat="server">
                                <div class="row">
                                    <asp:GridView ID="gvMAtrialInded" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        AutoGenerateColumns="False" ShowFooter="True" Width="501px"
                                        PageSize="15">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl.#">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                            <asp:TemplateField HeaderText=" resourcecode" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMatCode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="MTRF No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmtrref" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mtrref")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Resource Description">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgrcod" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbgvspecification" runat="server" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label13" runat="server"
                                                        Style="font-size: 11px; text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText=" Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmtrfqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mtrfqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
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
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

