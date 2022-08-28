
<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true"  CodeBehind="EmpAssessment.aspx.cs" Inherits="RealERPWEB.F_81_Hrm.F_81_Rec.EmpAssessment" %>

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
            <div class="card mt-5">
                <div class="card-header">
                <div class="row">
                  
                       <div class="col-lg-2 mt-3">
                            <div class="form-group">
                                
                                    <asp:Label ID="lblAssNo" runat="server" CssClass="lblTxt lblName">Assessment No :</asp:Label>
                                    <asp:Label ID="lblCurNo1" runat="server" CssClass="smLbl_to"></asp:Label>
                                    <asp:Label ID="lblCurNo2" runat="server" CssClass="smLbl_to"></asp:Label>
                            </div>
                           </div>
                    
                       <div class="col-lg-3">
                            <div class="form-group">
                                    <asp:Label ID="lblfrmdate" runat="server" CssClass="lblTxt lblName">Date</asp:Label>
                                    <asp:TextBox ID="txtCurDate" runat="server" CssClass=" form-control "></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                </div>
                           </div>
                     <div class="col-lg-3">
                            <div class="form-group">
                                    <asp:Label ID="lblRef" runat="server" CssClass="lblTxt lblName">Assessment Ref:</asp:Label>
                                    <asp:TextBox ID="txtassRef" runat="server" CssClass=" form-control"></asp:TextBox>
                                 </div>
                           </div>
                     <div class="col-lg-1 mt-3">
                            <div class="form-group">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                </div>
                           </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                    <asp:LinkButton ID="lbtnPrevAssNo" runat="server" CssClass="lblTxt lblName" OnClick="lbtnPrevAssNo_OnClick">Prev. Ass No:</asp:LinkButton>
                                    <asp:DropDownList ID="ddlPrevAssNo" runat="server" CssClass=" chzn-select form-control" >
                                    </asp:DropDownList>
                                </div>

                            </div>
                    </div>
                    <div class="row">
                         <div class="col-lg-1">
                            <div class="form-group">
                               
                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee List
                                        <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="fas fa-search" OnClick="ibtnEmpList_OnClick"></asp:LinkButton>
                                    </asp:Label>
                                    <asp:TextBox ID="txtEmpSrc" runat="server" CssClass="form-control"></asp:TextBox>
                                    <%--<asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="ibtnEmpList_OnClick"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>
                                </div>
                             </div>
                                <div class="col-lg-3 mt-3">
                                    <div class="form-group">
                                    <asp:DropDownList ID="ddlEmpName" runat="server" CssClass=" chzn-select form-control ">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblEmpname" runat="server" CssClass="form-control "   Visible="false"></asp:Label>
                                </div>
                               </div>
                                          
                    </div>
                <div class="card-body">

                    <asp:GridView ID="gvAssessment" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="1101px">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtasscod" runat="server" Style="text-align: left; font-size: 11px;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asscode")) %>'
                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description ">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assdesc"))%>'
                                            Width="500px" />
                                    </ItemTemplate>

                                    <FooterTemplate>
                                         <asp:LinkButton ID="lbtnUpPerAppraisal" runat="server" Font-Bold="True"
                                            CssClass="btn btn-primary btn-sm" OnClick="lbtnUpPerAppraisal_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        
                                        <asp:CheckBox ID="lblexec" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "exc"))=="True" %>'
                                            Width="80px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblgood" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "good"))=="True" %>'
                                            Width="80px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblavrg" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "avrg"))=="True" %>'
                                            Width="80px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblpoor" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "poor"))=="True" %>'
                                            Width="80px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="lblnill" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "nill"))=="True" %>'
                                            Width="80px" />
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
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
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


