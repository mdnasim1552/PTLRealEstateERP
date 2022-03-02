<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="ModeofPaymentDept.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_89_Pay.ModeofPaymentDept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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


            <div class="card card-fluid container-data" style="min-height: 500px;">
                <div class="card-header mt-3 mb-0 pb-0">
                    <div class="row mb-0 pb-0">
                        <asp:Label ID="lcomp" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Month</asp:Label>
                        <asp:DropDownList ID="ddlmon" data-placeholder="Choose Mon.." runat="server" OnSelectedIndexChanged="ddlmon_SelectedIndexChanged"
                            CssClass="chzn-select form-control col-1" AutoPostBack="true">
                        </asp:DropDownList>



                        <asp:Label ID="lbsection" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Dept</asp:Label>
                        <asp:DropDownList ID="ddldept" data-placeholder="Choose Department.." runat="server"
                            CssClass="chzn-select form-control col-2">
                        </asp:DropDownList>

                        
                        <asp:Label ID="lbbank" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Bank</asp:Label>
                        <asp:DropDownList ID="ddlbank" data-placeholder="Choose bank.." runat="server"
                            CssClass="chzn-select form-control col-1">
                        </asp:DropDownList>

                        <asp:Label ID="lblamt" runat="server" CssClass="btn btn-sm btn-secsondary mr-2 col-1">Amount</asp:Label>
                        <asp:TextBox ID="txtamt" runat="server" CssClass="form-control form-control-sm col-1 "></asp:TextBox>

                        <asp:LinkButton ID="lnkAdd" runat="server" OnClick="lnkAdd_Click" CssClass="btn btn-success btn-sm ml-1 col-1">Add</asp:LinkButton>

                    </div>


                </div>

                <div class="card-body">

                    <div class="row table table-responsive">
                        <asp:GridView ID="GvOtherDepSal" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowEditing="GvOtherDepSal_RowEditing"
                            OnRowCancelingEdit="GvOtherDepSal_RowCancelingEdit" OnRowUpdating="GvOtherDepSal_RowUpdating" ShowFooter="true">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">

                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />
                                </asp:TemplateField>

                             
                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" HeaderStyle-Width="50px"
                                    SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-edit&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;">
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle ForeColor="#0000C0" />
                                </asp:CommandField>

                                <asp:TemplateField HeaderText="Section">
                                    <ItemTemplate>
                                        
                                        <asp:Label ID="lblgvmonid" Visible="false" runat="server" Style="font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "monthid")) %>'
                                            BackColor="Transparent" BorderStyle="None"></asp:Label>
                                         <asp:Label ID="lblgvsection" Visible="false" runat="server" Style="font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secid")) %>'
                                            BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        <asp:Label ID="lblSection" runat="server" Style="font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            BackColor="Transparent" BorderStyle="None"></asp:Label>

                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bank Name">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbankcode" Visible="false" runat="server" Style="font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankcode")) %>'
                                            BackColor="Transparent" BorderStyle="None"></asp:Label>
                                        <asp:Label ID="lblbank" runat="server" Style="font-size: 11px;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bankname")) %>'
                                            BackColor="Transparent" BorderStyle="None"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="gvtxtamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString() %>'></asp:TextBox>

                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netpay")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                            </Columns>

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
