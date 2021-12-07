<%@ Page Title="" Language="C#" MasterPageFile="~/ASITNEW.Master" AutoEventWireup="true" CodeBehind="CivilConBOQ.aspx.cs" Inherits="RealERPWEB.F_07_Ten.CivilConBOQ" %>

<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
           $(".chzn-select").chosen(); $(".chzn-select-deselect").chosen({ allow_single_deselect: true });         

            //$('.chzn-select').chosen({ search_contains: true });
            //$(function () {
            //    $('[id*=DropCheck1]').multiselect({
            //        includeSelectAllOption: true
            //    });

            //});
        }



        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
    </script>
    <style>
        .chzn-container-single {
            width: 350px !important;
        }

            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

            body{
                font-family:'Century Gothic' !important;
            }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
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
            <div class="card card-fluid container-data mt-5" id='printarea'>
                <div class="card-body">
                    <div class="row mb-2" id="divFilter">
                        <div class="col-md-2 d-none">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Date</label>
                                </div>
                                <asp:TextBox ID="txtfodate" TabIndex="0" runat="server" CssClass="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtfodate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                   
                                    <label class="btn btn-secondary" type="button"> Projects List </label>
                                    
                                    
                                </div>
                                <div class="input-group-prepend">
                                    <a href="../F_07_Ten/TASCodeBooks?BookName=Project" class="btn btn-secondary" title="Add New Project" target="_blank"><i class="fas fa-plus-octagon"></i></a>
                                </div>                                

                                <asp:DropDownList ID="ddlProject" TabIndex="1" runat="server" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" AutoPostBack="True" CssClass="chzn-select  form-control  pl-0 pr-0">
                                </asp:DropDownList>

                            </div>
                        </div>


                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Profit Rate %</label>
                                </div>
                                <asp:TextBox ID="txtSbtRate_Per" TabIndex="2" onkeypress="return isNumberKey(this, event);" runat="server" CssClass="form-control">0</asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Overhead %</label>
                                </div>
                                <asp:TextBox ID="txtACCost_Per" TabIndex="3" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control">0</asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Taxt & Vat  %</label>
                                </div>
                                <asp:TextBox ID="txtACCostVatOH_Per" TabIndex="4" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control   pr-0">0</asp:TextBox>

                            </div>
                        </div>


                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkbtnOK" runat="server" OnClick="lnkbtnOK_Click" CssClass="btn btn-primary btn-md primaryBtn">Ok</asp:LinkButton>

                        </div>
                        <div class="col-md-3 d-none">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <asp:LinkButton ID="ImgbtnFindReq" OnClick="ImgbtnFindReq_Click" runat="server" class="btn btn-secondary">Previous</asp:LinkButton>
                                   
                                </div>
                                 <asp:DropDownList ID="ddlPrevReqList" OnSelectedIndexChanged="ddlPrevReqList_SelectedIndexChanged" AutoPostBack="true" runat="server" Style="width: 200px;" CssClass="chzn-select smDropDown inputTxt" TabIndex="11">
                                                </asp:DropDownList>
                                <%--<asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>
                        </div>
                    </div>






                    <div class="row mb-2" runat="server" id="divResource" visible="false">

                       
                        <div class="col-md-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Work Group</button>
                                </div>
                                <div class="input-group-prepend">
                                     
                                    <a href="../F_17_Acc/AccSubCodeBook?InputType=Wrkschedule" class="btn btn-secondary" title="Add New Work" target="_blank"><i class="fas fa-plus-octagon"></i></a>
                                </div>
                                <asp:DropDownList ID="ddlWorkGroup" OnSelectedIndexChanged="ddlWorkGroup_SelectedIndexChanged" runat="server" AutoPostBack="True" CssClass="   form-control  pl-0 pr-0">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-5">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Work List</button>
                                </div>
                                <asp:DropDownList ID="ddlWorkList" runat="server" AutoPostBack="True" CssClass="chzn-select form-control  pl-0 pr-0">
                                </asp:DropDownList>

                                <%--<asp:ListBox ID="DropCheck1" runat="server" CssClass="form-control" Style="min-width: 100px !important;" SelectionMode="Multiple"></asp:ListBox>--%>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <button class="btn btn-secondary" type="button">Page</button>
                                </div>

                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                    Font-Bold="True" Font-Size="12px" CssClass="form-control  pl-0 pr-0"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="150">150</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <asp:LinkButton ID="lnkbtnAdd" runat="server" OnClick="lnkbtnAdd_Click" CssClass="btn btn-primary btn-md primaryBtn">Add</asp:LinkButton>

                        </div>
                        
                    </div>

                    <div class="row mb-2">
                        <div class="table-responsive">
                        <asp:GridView ID="gvCivilBoq" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" Visible="false" HeaderStyle-VerticalAlign="Middle"
                            OnPageIndexChanging="gvCivilBoq_PageIndexChanging" PageSize="20"
                            ShowFooter="True" CssClass="table-condensed table-hover table-bordered grvContentarea" OnRowCreated="gvCivilBoq_RowCreated">
                            <PagerSettings PageButtonCount="20" Mode="NumericFirstLast" />
                            <RowStyle Font-Size="11px" />
                            <Columns>

                                <asp:TemplateField HeaderText="#">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="False" Font-Size="12px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" VerticalAlign="Middle" HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" ">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDelrow" Width="20px" ForeColor="Red" runat="server" OnClick="lnkDelrow_Click" ToolTip="Delete"><i class=" fa fa-trash"></i></a></asp:LinkButton>


                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" Width="20px" VerticalAlign="Middle" />
                                    <HeaderStyle Width="20px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="" Visible="false">
                                    <ItemTemplate>
                                       <%-- <asp:Label ID="lblbyid" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemid")) %>'
                                            Width="30px"></asp:Label>--%>
                                        <asp:Label ID="lbconvrate" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "convrate")) %>'
                                            Width="30px"></asp:Label>
                                        <asp:Label ID="lblgvactcode" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="30px"></asp:Label>
                                        <asp:Label ID="lblGvworkcode" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" VerticalAlign="Middle" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Work <br> Description">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsubdesc" runat="server"  CssClass="pl-1 pr-1" Font-Bold="False" Font-Size="12px" Width="450px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim().Length>0 ?  "<br>" : "")+                                                                                                                                      
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "sdetails")).Trim(): "")                                                                          
                                                                    %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Justify" VerticalAlign="Middle" />

                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle Font-Bold="true" Font-Size="14px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item Code">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txItemCode" Width="80px" OnTextChanged="txItemCode_TextChanged" Style="background: none;" runat="server" Font-Size="12px" Height="28px" CssClass="form-control"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itemcode")) %>'></asp:TextBox>
                                    </ItemTemplate>

                                    <ItemStyle Font-Size="12px" VerticalAlign="Middle" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtqty" Width="80px" Style="background: none; text-align: right" onkeypress="return isNumberKey(this,event);" OnTextChanged="txtqty_TextChanged" 
                                            AutoPostBack="true" runat="server" Font-Size="12px" Height="28px" CssClass="form-control"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                    </ItemTemplate>

                                    <ItemStyle Font-Size="12px" VerticalAlign="Middle" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server" Font-Bold="False" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" VerticalAlign="Middle" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtrate" Width="80px" Style="background: none; text-align: right" OnTextChanged="txtrate_TextChanged" onkeypress="return isNumberKey(this,event);"
                                            AutoPostBack="true" runat="server" Font-Size="12px" Height="28px" CssClass="form-control"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>


                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount <br> in <br> TK">
                                     <FooterTemplate>
                                        <asp:Label ID="lblgvFAmtInTk" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblordam" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual <br> Cost">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsbamt" Width="80px" Style="background: none; text-align: right" OnTextChanged="txtsbamt_TextChanged" onkeypress="return isNumberKey(this,event);"
                                            AutoPostBack="true" runat="server" Font-Size="12px" Height="28px" CssClass="form-control"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sbtamt")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Profit " Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProfit" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sbtrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" VerticalAlign="Middle" HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Overhead  <br> Rate" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvOH" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ohamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Submit <br> Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblttlamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Vat + Tax" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvVatTax" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taxvatamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual Cost <br> with <br>  Vat+OH">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFcostvatoh" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcostvatoh" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "costvatoh")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Actual  <br> Amount">

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFactamt" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                    </FooterTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvactamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Difference <br> Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFdiffamt" runat="server" BackColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdiffamt" runat="server" BackColor="Transparent" BorderStyle="None"
                                            BorderWidth="1px" Font-Size="11px" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "diffamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Right" VerticalAlign="Middle" />
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



        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
