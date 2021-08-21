<%@ Page Title="" Language="C#" MasterPageFile="~/ASITMaster.Master" AutoEventWireup="true" CodeBehind="CreateOfferLt.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.CreateOfferLt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function ShowModal() {
            $("#insertModal").modal('toggle');
        }
    </script>
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
                    <div class="form-horizontal">

                        <div class="form-group">

                            <div class="col-md-8">
                                <asp:Label ID="lblOfferNo" runat="server" CssClass="lblTxt lblName">Offer Letter No :</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                                <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                <asp:Label ID="lblRef" runat="server" CssClass="lblTxt lblName">Offer Ref:</asp:Label>
                                <asp:TextBox ID="txtOffRef" runat="server" CssClass=" inputDateBox "></asp:TextBox>
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_OnClick">Ok</asp:LinkButton>
                            </div>
                            <div class="col-md-4">
                                <asp:LinkButton ID="lbtnPrevOffLtNo" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevOffLtNo_OnClick">Prev. Offer No:</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevOffLtNo" runat="server" Width="200" CssClass="form-control inputTxt pull-left" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                            <%--<asp:Label ID="lblOffRef" runat="server" CssClass="lblTxt lblName">Offer Ref No :</asp:Label>--%>
                        </div>
                    </div>
                </fieldset>
                <div class="col-md-12">
                    <asp:GridView ID="gvOfferltr" runat="server" AutoGenerateColumns="False"
                        ShowFooter="True" CssClass="table-striped table-hover table-bordered table-responsive grvContentarea col-md-10" OnRowDataBound="gvOfferltr_RowDataBound">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">

                                <ItemTemplate>
                                    <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtoffcod" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtdesc" TextMode="MultiLine"
                                        runat="server" Style="text-align: left"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "descp")) %>'
                                        Width="700px" BackColor="Transparent" BorderStyle="None" Font-Size="11px" Height="40px"></asp:TextBox>

                                    <asp:LinkButton ID="btnAdd" runat="server" CssClass="btn btn-sm btn-success" OnClick="btnAdd_Click">Add Salary Info</asp:LinkButton>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lUpdate_OnClick">Update</asp:LinkButton>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>

                </div>

                <div class="modal fade" id="insertModal" role="dialog" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                                <h4 class="modal-title text-center">Updating Salary Information</h4>
                            </div>
                            <div class="modal-body">
                                <div class="form-group">
                                    <label class="control-label">Category</label>
                                    <asp:TextBox ID="txtcat" runat="server" CssClass="form-control"></asp:TextBox>
                                   
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Rank</label>
                                    <asp:TextBox ID="txtgrade" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                 <div class="form-group">
                                    <label class="control-label">Designation</label>
                                    <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                <div class="form-group">
                                    <label class="control-label">Gross Salary</label>
                                    <asp:TextBox ID="txtgross" runat="server" CssClass="form-control"></asp:TextBox>
                                    
                                </div>
                                
                                  <div class="form-group">
                                    <label class="control-label">Remarks</label>
                                      <asp:TextBox ID="txtrmks" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="modal-footer">
                            <%--    <button class="btn btn-success" data-dismiss="modal" aria-hidden="true" >Update</button>--%>
                                <asp:LinkButton ID="btnSalaryupdate" runat="server" class="btn btn-success" aria-hidden="true" OnClick="btnSalaryupdate_Click">Update</asp:LinkButton>
                                <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                            </div>
                        </div>
                    </div>
                </div>


            </div>


        </div>
    </div>
    
</asp:Content>


